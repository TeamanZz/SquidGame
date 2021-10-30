using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenBazooka : WardenBase
{
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    public float damage = 1;
    public Transform shootPoint;
    public float projectileSpeed = 2;

    private void Start()
    {
        StartCoroutine(ShootRepeatedely());
    }

    private IEnumerator ShootRepeatedely()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            if (Target != null)
            {
                if (canShoot)
                {
                    var newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y - 40, transform.localEulerAngles.z));
                    newBullet.GetComponent<BazookaProjectile>().damage = damage;
                    newBullet.GetComponent<BazookaProjectile>().parentWarden = this;
                    var forceVector = Target.transform.position - shootPoint.position;
                    forceVector += new Vector3(0, 0.4f, 0);
                    newBullet.GetComponent<Rigidbody>().AddForce(forceVector.normalized * projectileSpeed, ForceMode.Impulse);

                }
            }
        }
    }
}