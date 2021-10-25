using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Escaper : MonoBehaviour
{
    public Transform endGate;
    private NavMeshAgent agent;
    private float maxHealth;
    public float currentHealth = 5;
    public Image healthBar;
    public GameZone gameZone;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
        agent = GetComponent<NavMeshAgent>();
        endGate = ZoneManager.Instance.GetGate(gameZone);
        agent.SetDestination(endGate.position);
    }

    public void DescreaseHealth(float value)
    {
        currentHealth -= value;
        healthBar.fillAmount -= ((float)value / (float)maxHealth);
        if (currentHealth <= 0)
        {
            ZoneManager.Instance.RemoveEscaperFromList(this, gameZone);
            Destroy(gameObject);
        }
    }
}
