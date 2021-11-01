using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardenBase : MonoBehaviour
{
    public GameZone gameZone;
    public float expValue;
    public Transform expBar;
    public Image expBarImage;
    public bool canShoot = true;
    public bool canMerge;
    public int hp;
    public List<GameObject> canMergeParticles = new List<GameObject>();
    public int wardenStrenght;
    private EscaperBase target;
    public EscaperBase Target
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

    public void DecreaseHP(KnifeEscaper knifeEscaper)
    {
        hp--;
        if (hp <= 0)
        {
            ZoneManager.Instance.RemoveWardenFromList(this);
            knifeEscaper.GoToGates();
            Destroy(gameObject);
        }
    }

    public void SetTarget()
    {
        if (canShoot)
        {
            Target = ZoneManager.Instance.GetNearestToGateEscaper(gameZone);

            if (Target != null)
                GetComponent<Animator>().SetBool("HaveTarget", true);
        }
    }

    private void Update()
    {
        if (Target != null)
            LookAtTarget();

        RotateExpBarToCamera();

        Mathf.Clamp(transform.position.x, -2.2f, 2.2f);
        Mathf.Clamp(transform.position.z, -3.5f, 3);
    }

    public void IncreaseExpBarValue()
    {
        expValue = Mathf.Clamp(expValue + 0.2f, 0, 1);
        if (expValue >= 1)
        {
            canMergeParticles[0].SetActive(true);
            canMergeParticles[1].SetActive(true);
            canMerge = true;
        }
        expBarImage.fillAmount = expValue;
    }


    private void RotateExpBarToCamera()
    {
        expBar.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void LookAtTarget()
    {
        Vector3 relativePos = Target.transform.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y + 55, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Y, 4 * Time.deltaTime);
    }
}