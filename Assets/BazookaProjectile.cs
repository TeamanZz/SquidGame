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

    private void OnTriggerEnter(Collider other)
    {
        Escaper escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<Escaper>(out escaper))
        {
            wasCollided = true;
            // escaper.DescreaseHealth(damage, parentWarden);
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
                rb.GetComponent<Escaper>().Explode();
            }
        }
    }
}