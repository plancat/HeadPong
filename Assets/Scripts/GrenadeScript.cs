using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour
{

    [Header("Timer")]
    //Time before the grenade explodes
    public float grenadeTimer = 5.0f;

    [Header("Explosion Prefabs")]
    //All explosion prefabs
    public GameObject explosion;

    [Header("Explosion Options")]
    public float radius = 25.0F;
    public float power = 350.0F;

    [Header("Throw Force")]
    public float minimumForce = 350.0f;
    public float maximumForce = 850.0f;
    float throwForce;

    [Header("Audio")]
    public AudioSource impactSound;

    void OnCollisionEnter(Collision collision)
    {
        impactSound.Play();

        StartCoroutine(ExplosionTimer());
    }

    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(grenadeTimer);

        GameObject tempEx = Instantiate(explosion);
        tempEx.transform.position = transform.position;
        Destroy(tempEx, 3.0f);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if (rb.transform.CompareTag("Enemy"))
                {
                    var enemy = rb.GetComponent<Enemy>();
                    enemy.Hit(rb.transform.position);
                    enemy.Hit(rb.transform.position);
                    enemy.Hit(rb.transform.position);
                    enemy.Hit(rb.transform.position);
                    enemy.Hit(rb.transform.position);
                }
            }
        }

        Destroy(gameObject);
    }
}