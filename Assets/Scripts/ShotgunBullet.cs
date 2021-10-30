using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public WardenBase parentWarden;
    public bool wasCollided;

    private void OnTriggerEnter(Collider other)
    {
        Escaper escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<Escaper>(out escaper))
        {
            wasCollided = true;
            escaper.PushBack();
            Destroy(gameObject);
        }
    }
}