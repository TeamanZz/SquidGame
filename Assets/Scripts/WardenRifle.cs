using System.Collections;
using UnityEngine;

public class WardenRifle : WardenBase
{
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    public float damage = 1;
    public Transform shootPoint;

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
                    var newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(-90, 0, -90));
                    newBullet.GetComponent<Bullet>().damage = damage;
                    newBullet.GetComponent<Bullet>().parentWarden = this;
                    var forceVector = Target.transform.position - shootPoint.position;
                    forceVector += new Vector3(0, 0.4f, 0);
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