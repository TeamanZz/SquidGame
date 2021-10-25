using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warden : MonoBehaviour
{
    public GameZone gameZone;
    public Escaper target;
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    public float damage = 1;
    public bool canShoot = true;
    public int wardenStrenght;

    private void Start()
    {
        StartCoroutine(ShootRepeatedely());
    }

    public void SetTarget()
    {
        target = ZoneManager.Instance.GetNearestToGateEscaper(gameZone);
    }

    private IEnumerator ShootRepeatedely()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            if (target != null)
            {
                if (canShoot)
                {
                    var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().damage = damage;
                    var forceVector = target.transform.position - transform.position;
                    newBullet.GetComponent<Rigidbody>().AddForce(forceVector * 5, ForceMode.Impulse);
                }
            }
        }
    }
}

public enum GameZone
{
    Left,
    Right
}