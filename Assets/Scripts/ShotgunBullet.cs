using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public WardenBase parentWarden;
    public bool wasCollided;
    public float damage;


    private void Start()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }

    private IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        EscaperBase escaper;
        if (!wasCollided && other.gameObject.TryGetComponent<EscaperBase>(out escaper))
        {
            wasCollided = true;
            escaper.PushBack();
            escaper.DescreaseHealth(damage, parentWarden);
            Destroy(gameObject);
        }
    }
}