using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Warden : MonoBehaviour
{
    public GameZone gameZone;
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    public float damage = 1;
    public bool canShoot = true;
    public int wardenStrenght;
    public Transform shootPoint;

    private Escaper target;
    public Escaper Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
            if (target == null)
                GetComponent<Animator>().SetBool("HaveTarget", false);
            else
                GetComponent<Animator>().SetBool("HaveTarget", true);
        }
    }

    private void Start()
    {
        StartCoroutine(ShootRepeatedely());
    }

    public void SetTarget()
    {
        Target = ZoneManager.Instance.GetNearestToGateEscaper(gameZone);

        if (Target != null)
            GetComponent<Animator>().SetBool("HaveTarget", true);
    }

    private void Update()
    {
        if (Target != null)
        {
            Vector3 relativePos = Target.transform.position - transform.position;
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
            if (Target != null)
            {
                if (canShoot)
                {
                    var newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().damage = damage;
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