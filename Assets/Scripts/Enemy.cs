using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider collider;
    private NavMeshAgent agent;

    private AudioSource hitSound;

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
        hitSound = GetComponent<AudioSource>();
    }

    public void Live()
    {
        agent.enabled = true;
        collider.enabled = true;
        anim.Play("Walk");
    }

    void Update()
    {
        if (!IsHit && health > 0)
        {
            float dist = Vector3.Distance(transform.position, Player.GetInstance.transform.position);
            
            if (dist < 1.0f)
            {
                agent.enabled = false;
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
                agent.enabled = true;
                agent.SetDestination(Player.GetInstance.gameObject.transform.position);
            }
        }
    }

    public void Hit(Vector3 point)
    {
        if (health <= 0)
            return;

        IsHit = true;

        agent.enabled = false;

        hitSound.Play();

        StopCoroutine("HitDelay");
        StartCoroutine("HitDelay");

        int at = Random.Range(1, 3);
        anim.SetTrigger("Hit");
        anim.SetInteger("HitType", at);

        health -= 1;
        if (health <= 0)
        {
            Player.GetInstance.cash += 250;
            agent.enabled = false;
            collider.enabled = false;
            anim.SetTrigger("Dead");
            StartCoroutine("DeathDelay");
        }

        GameObject h = Instantiate(bloodPrefab);
        h.transform.position = point;
        Destroy(h, 1.0f);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
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
        if (dist < 1.2f)
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