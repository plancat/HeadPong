using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider collider;
    private NavMeshAgent agent;

    private bool IsAttack = false;
    private bool IsHit = false;

    public int health = 3;

    [SerializeField]
    private float attackDelay = 0.0f;
    [SerializeField]
    private float attackDamageDelay = 0.0f;

    public GameObject bloodPrefab;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!IsHit && health > 0)
        {
            if (Player.GetInstance.health > 0)
            {
            }

            float dist = Vector3.Distance(transform.position, Player.GetInstance.transform.position);


            if (dist < 1.5f)
            {
                agent.Stop();
                if (!IsAttack)
                {
                    IsAttack = true;
                    StartCoroutine(AttackDelay());

                    int at = Random.Range(1, 4);
                    anim.SetTrigger("Attack");
                    anim.SetInteger("AttackType", at);

                    StartCoroutine(AttackDamageDelay());
                }
            }
            else
            {
                agent.SetDestination(Player.GetInstance.gameObject.transform.position);
            }
        }
    }

    public void Hit(Vector3 point)
    {
        IsHit = true;

        agent.Stop();

        StopCoroutine("HitDelay");
        StartCoroutine("HitDelay");

        int at = Random.Range(1, 3);
        anim.SetTrigger("Hit");
        anim.SetInteger("HitType", at);

        health -= 1;
        if (health <= 0)
        {
            agent.enabled = false;
            collider.enabled = false;

            anim.SetTrigger("Dead");
        }

        GameObject h = Instantiate(bloodPrefab);
        h.transform.position = point;
        Destroy(h, 1.0f);
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        IsAttack = false;
    }

    IEnumerator AttackDamageDelay()
    {
        yield return new WaitForSeconds(attackDamageDelay);

        float dist = Vector3.Distance(transform.position, Player.GetInstance.transform.position);
        if (dist < 1.6f)
        {
            Player.GetInstance.Hit();
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.4f);
        IsHit = false;
    }
}