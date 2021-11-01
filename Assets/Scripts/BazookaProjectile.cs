using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaProjectile : MonoBehaviour
{
    [HideInInspector] public float damage = 1;
    public WardenBase parentWarden;
    public bool wasCollided;
    public GameObject explosionParticles;
    public Vector3 rotation;
    public float explodeRadius = 1;
    public float explosionForce;
    public Vector3 shootPointPosition;

    private void OnTriggerEnter(Collider other)
    {
        EscaperBase escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<EscaperBase>(out escaper))
        {
            wasCollided = true;
            Explode();
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null && rb.GetComponent<BazookaProjectile>() == null && rb.GetComponent<WardenBase>() == null)
            {
                if (rb.GetComponent<EscaperBase>() == null)
                    return;

                rb.GetComponent<EscaperBase>().Explode();
                rb.GetComponent<EscaperBase>().DescreaseHealth(damage, parentWarden);
            }
        }
    }
}