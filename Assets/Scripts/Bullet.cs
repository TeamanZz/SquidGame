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
        EscaperBase escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<EscaperBase>(out escaper))
        {
            wasCollided = true;
            escaper.DescreaseHealth(damage, parentWarden);
            Destroy(gameObject);
        }
    }
}