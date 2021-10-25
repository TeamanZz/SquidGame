using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenSpawner : MonoBehaviour
{
    public static WardenSpawner Instance;

    public List<GameObject> wardenPrefabs = new List<GameObject>();

    public float minX;
    public float maxX;

    public float minZ;
    public float maxZ;

    public float yPos;

    public int wardenCost;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnWardenOnMerge(Vector3 position, int newWardenStrenght)
    {
        var newWardenGameObject = Instantiate(wardenPrefabs[newWardenStrenght], position, Quaternion.identity);
        var newWarden = newWardenGameObject.GetComponent<Warden>();
        ZoneManager.Instance.AddWardenToList(newWarden);
    }

    public void SpawnWardenOnButton()
    {
        if (ZoneManager.Instance.moneyCount < wardenCost)
            return;
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ));
        var newWardenGameObject = Instantiate(wardenPrefabs[0], spawnPoint, Quaternion.Euler(0, 180, 0));
        var newWarden = newWardenGameObject.GetComponent<Warden>();

        if (newWarden.transform.position.x > 0)
            newWarden.gameZone = GameZone.Right;
        else
            newWarden.gameZone = GameZone.Left;

        ZoneManager.Instance.AddWardenToList(newWarden);
        ZoneManager.Instance.moneyCount -= wardenCost;
        ZoneManager.Instance.UpdateMoneyCount();
    }
}