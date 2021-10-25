using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Warden : MonoBehaviour
{
    public GameZone gameZone;
    public Escaper target;
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    public float damage = 1;
    public bool canShoot = true;
    public int wardenStrenght;
    public Transform shootPoint;

    private void Start()
    {
        StartCoroutine(ShootRepeatedely());
    }

    public void SetTarget()
    {
        target = ZoneManager.Instance.GetNearestToGateEscaper(gameZone);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);

            Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y + 55, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Y, 4 * Time.deltaTime);
        }
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
                    var newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().damage = damage;
                    var forceVector = target.transform.position - shootPoint.position;
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