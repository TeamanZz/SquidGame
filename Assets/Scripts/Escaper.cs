using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using DG.Tweening;

public class Escaper : MonoBehaviour
{
    public Transform endGate;
    private NavMeshAgent agent;
    private float maxHealth;
    public float currentHealth = 5;
    public Image healthBar;
    public GameZone gameZone;
    public GameObject deathParticles;

    public bool isLastEnemy;
    public Barrier barrier;

    public float pushBackForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
        agent = GetComponent<NavMeshAgent>();
        endGate = ZoneManager.Instance.GetGate(gameZone);
        agent.SetDestination(endGate.position);
    }

    public void PushBack()
    {
        StartCoroutine(IEPushBack());
    }

    IEnumerator IEPushBack()
    {
        var forceVector = new Vector3(transform.position.x, 0, -6) - transform.position;
        forceVector += new Vector3(0, 0.4f, 0); // Это сделано для того чтобы летело в середну бегуна, а не ему в ноги
        if (!agent.isStopped)
            agent.isStopped = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(forceVector * pushBackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody>().isKinematic = true;
        agent.isStopped = false;
    }

    public void DescreaseHealth(float value, WardenBase warden)
    {
        currentHealth -= value;
        healthBar.fillAmount -= ((float)value / (float)maxHealth);
        if (currentHealth <= 0)
        {
            if (warden != null)
                warden.IncreaseExpBarValue();
            ZoneManager.Instance.RemoveEscaperFromList(this, gameZone);
            Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Barrier>(out barrier))
        {
            barrier.crushers.Add(this);
            agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsFighting", true);
        }
    }

    public void DecreaseBarrierHP()
    {
        if (barrier != null)
            barrier.DecreaseHP();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Barrier>(out barrier))
        {
            barrier.crushers.Remove(this);
            agent.isStopped = false;
            GetComponent<Animator>().SetBool("IsFighting", false);
        }
    }

    public void OnBarrierBroke()
    {
        if (agent != null)
            agent.isStopped = false;
        GetComponent<Animator>().SetBool("IsFighting", false);
    }
}