using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsHandler : MonoBehaviour
{
    public static SpellsHandler Instance;

    public CenterWall centerWall;

    public List<GameObject> spikes = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();

    public bool wallRemoved = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnSpikes()
    {
        spikes[Random.Range(0, 2)].SetActive(true);
    }

    public void SpawnObstacles()
    {
        obstacles[Random.Range(0, 2)].SetActive(true);
    }

    public void RemoveWall()
    {
        wallRemoved = true;
        centerWall.RemoveWall();
    }
}