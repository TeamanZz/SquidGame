using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float damage = 1;
    public WardenBase parentWarden;
    public bool wasCollided;

    private void OnTriggerEnter(Collider other)
    {
        Escaper escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<Escaper>(out escaper))
        {
            wasCollided = true;
            escaper.DescreaseHealth(damage, parentWarden);
            Destroy(gameObject);
        }
    }
}