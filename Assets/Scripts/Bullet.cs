using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        Escaper escaper;
        if (other.gameObject.TryGetComponent<Escaper>(out escaper))
        {
            escaper.DescreaseHealth(damage);
            Destroy(gameObject);
        }
    }
}
