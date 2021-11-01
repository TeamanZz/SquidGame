using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EscaperBase : MonoBehaviour
{
    public Transform endGate;
    public NavMeshAgent agent;
    public float maxHealth;
    public float currentHealth = 5;
    public Image healthBar;
    public GameZone gameZone;
    public GameObject deathParticles;

    public GameObject shockParticles;

    public bool isLastEnemy;
    public Barrier barrier;

    public float pushBackForce = 2;

    protected void Start()
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

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Barrier>(out barrier))
        {
            barrier.crushers.Add(this);
            if (agent.enabled == true)
                agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsFighting", true);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Barrier>(out barrier))
        {
            barrier.crushers.Remove(this);
            if (agent.enabled == true)
                agent.isStopped = false;
            GetComponent<Animator>().SetBool("IsFighting", false);
        }
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

    public void DescreaseHealth(float value)
    {
        currentHealth -= value;
        healthBar.fillAmount -= ((float)value / (float)maxHealth);
        if (currentHealth <= 0)
        {
            ZoneManager.Instance.RemoveEscaperFromList(this, gameZone);
            Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator IEPushBack()
    {
        var forceVector = new Vector3(transform.position.x, 0, -6) - transform.position;
        forceVector += new Vector3(0, 0.4f, 0); // Это сделано для того чтобы летело в середну бегуна, а не ему в ноги
        if (agent.enabled)
            agent.isStopped = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(forceVector.normalized * pushBackForce, ForceMode.Impulse);
        shockParticles.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody>().isKinematic = true;
        shockParticles.SetActive(false);
        if (agent.enabled)
            agent.isStopped = false;
    }

    public void DecreaseBarrierHP()
    {
        if (barrier != null)
            barrier.DecreaseHP();
    }

    public void OnBarrierBroke()
    {
        if (agent != null)
            agent.isStopped = false;
        GetComponent<Animator>().SetBool("IsFighting", false);
    }
    public void Explode()
    {
        StartCoroutine(IEExplode());
    }

    private IEnumerator IEExplode()
    {
        agent.enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddExplosionForce(250, transform.position, 1);
        yield return new WaitForSeconds(0.4f);
        GetComponent<Rigidbody>().isKinematic = true;
        agent.enabled = true;
        agent.SetDestination(endGate.position);
        GetComponent<Rigidbody>().useGravity = false;
    }
}