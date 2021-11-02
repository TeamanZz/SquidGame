using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEscaper : EscaperBase
{
    public WardenBase targetWarden;
    private bool needRunForWarden = true;
    private new void Start()
    {
        base.Start();
        InvokeRepeating("FollowWarden", 2, 3);
    }

    private void Update()
    {
        if (targetWarden != null)
        {

            LookAtTarget();
            if (agent.enabled)
                base.agent.destination = targetWarden.transform.position;
            if (!targetWarden.canShoot)
            {
                GoToGates();
            }
        }
    }

    private void LookAtTarget()
    {
        Vector3 relativePos = targetWarden.transform.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Y, 4 * Time.deltaTime);
    }

    public void GoToGates()
    {
        targetWarden = null;
        if (agent.enabled == true)
            agent.isStopped = false;
        GetComponent<Animator>().SetBool("IsFighting", false);
        agent.SetDestination(endGate.position);
    }

    public void PunchTarget()
    {
        if (targetWarden != null)
        {
            targetWarden.DecreaseHP(this);
        }

    }

    public void FollowWarden()
    {
        if (targetWarden == null && needRunForWarden)
        {
            targetWarden = ZoneManager.Instance.GetRandomWarden(base.gameZone);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<WardenBase>(out targetWarden))
        {
            if (agent.enabled == true)
                agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsFighting", true);
        }
    }

    private new void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Barrier>(out barrier))
        {
            needRunForWarden = false;
            barrier.crushers.Add(this);
            if (agent.enabled == true)
                agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsFighting", true);
        }
    }
}