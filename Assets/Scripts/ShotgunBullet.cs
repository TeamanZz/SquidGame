using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public WardenBase parentWarden;
    public bool wasCollided;

    private void OnTriggerEnter(Collider other)
    {
        EscaperBase escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<EscaperBase>(out escaper))
        {
            wasCollided = true;
            escaper.PushBack();
            Destroy(gameObject);
        }
    }
}