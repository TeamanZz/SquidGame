using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsHandler : MonoBehaviour
{
    public List<GameObject> spikes = new List<GameObject>();

    public void SpawnSpikes()
    {
        spikes[Random.Range(0, 2)].SetActive(true);
    }
}