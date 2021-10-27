using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenSpawner : MonoBehaviour
{
    public static WardenSpawner Instance;

    public GameObject mergeParticles;
    public GameObject spawnParticles;

    public Transform sniperSpawnPointLeft;
    public Transform sniperSpawnPointRight;
    public GameObject sniperPrefab;

    public int sniperWardenCost;
    public int wardenCost;
    public List<GameObject> wardenPrefabs = new List<GameObject>();

    private float minX = -2;
    private float maxX = 2;

    private float minZ = -5;
    private float maxZ = 5;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnWardenOnMerge(Vector3 position, int newWardenStrenght)
    {
        var newWardenGameObject = Instantiate(wardenPrefabs[newWardenStrenght], position, Quaternion.identity);
        Instantiate(mergeParticles, new Vector3(position.x, position.y - 0.1f, position.z), Quaternion.identity);
        var newWarden = newWardenGameObject.GetComponent<Warden>();
        ZoneManager.Instance.AddWardenToList(newWarden);
    }

    public void SpawnWardenOnButton()
    {
        if (ZoneManager.Instance.moneyCount < wardenCost)
            return;
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        var newWardenGameObject = Instantiate(wardenPrefabs[0], spawnPoint, Quaternion.Euler(0, 180, 0));
        Instantiate(spawnParticles, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.identity);
        var newWarden = newWardenGameObject.GetComponent<Warden>();

        if (newWarden.transform.position.x > 0)
            newWarden.gameZone = GameZone.Right;
        else
            newWarden.gameZone = GameZone.Left;

        ZoneManager.Instance.AddWardenToList(newWarden);
        ZoneManager.Instance.moneyCount -= wardenCost;
        ZoneManager.Instance.UpdateMoneyCount();
    }

    public void SpawnSniperWardenOnButton()
    {
        if (ZoneManager.Instance.sniperWardens.Count > 1)
        {
            return;
        }

        if (ZoneManager.Instance.moneyCount < sniperWardenCost)
            return;

        Vector3 spawnPosition = new Vector3();
        GameZone sniperGameZone;
        if (ZoneManager.Instance.sniperWardens.Count > 0)
        {
            sniperGameZone = GameZone.Right;
            spawnPosition = sniperSpawnPointRight.position;
        }
        else
        {
            sniperGameZone = GameZone.Left;
            spawnPosition = sniperSpawnPointLeft.position;
        }

        var newWardenGameObject = Instantiate(sniperPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
        var newWarden = newWardenGameObject.GetComponent<SniperWarden>();
        newWarden.gameZone = sniperGameZone;

        ZoneManager.Instance.AddWardenToList(newWarden);
        ZoneManager.Instance.moneyCount -= sniperWardenCost;
        ZoneManager.Instance.UpdateMoneyCount();




    }
}