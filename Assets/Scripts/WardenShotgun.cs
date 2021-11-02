using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenShotgun : WardenBase
{
    public float timeBetweenShots = 0.5f;
    public GameObject ammoPrefab;
    public Transform shootPoint;
    public float damage = 1;

    private void Start()
    {
        StartCoroutine(ShootRepeatedely());
    }

    private IEnumerator ShootRepeatedely()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);

            if (Target != null && canShoot)
            {
                var newBullet = Instantiate(ammoPrefab, shootPoint.position, Quaternion.Euler(-90, 0, -90));
                GetComponent<Animator>().Play("Shoot");
                newBullet.GetComponent<ShotgunBullet>().parentWarden = this;
                newBullet.GetComponent<ShotgunBullet>().damage = damage;
                var forceVector = Target.transform.position - shootPoint.position;
                forceVector += new Vector3(0, 0.4f, 0); // Это сделано для того чтобы летело в середну бегуна, а не ему в ноги
                newBullet.GetComponent<Rigidbody>().AddForce(forceVector * 5, ForceMode.Impulse);
            }
        }
    }
}