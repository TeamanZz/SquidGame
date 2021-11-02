using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WardenSpawner : MonoBehaviour
{
    public static WardenSpawner Instance;

    public GameObject mergeParticles;
    public GameObject spawnParticles;

    public Transform sniperSpawnPointLeft;
    public Transform sniperSpawnPointRight;
    public GameObject sniperPrefab;

    [Header("Rifle, Shotgun, Bazooka")]
    public List<int> wardensCost = new List<int>();
    public List<TextMeshProUGUI> wardensCostText = new List<TextMeshProUGUI>();

    [Header("Sniper")]
    public int wardenSniperCost;
    public TextMeshProUGUI wardenSniperCostText;

    public List<GameObject> wardenTypesPrefabs = new List<GameObject>();

    private float minX = -2.2f;
    private float maxX = 2.2f;

    private float minZ = -3.5f;
    private float maxZ = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < wardensCost.Count; i++)
        {
            wardensCostText[i].text = wardensCost[i].ToString() + " $";
        }

        wardenSniperCostText.text = wardenSniperCost.ToString() + " $";
    }

    public void ColorifyCostText(int currentMoneyCount)
    {
        for (int i = 0; i < wardensCost.Count; i++)
        {
            if (currentMoneyCount >= wardensCost[i])
            {
                wardensCostText[i].color = Color.green;
            }
            else
            {
                wardensCostText[i].color = Color.white;
            }
        }

        if (currentMoneyCount >= wardenSniperCost)
        {
            wardenSniperCostText.color = Color.green;
        }
        else
        {
            wardenSniperCostText.color = Color.white;
        }
    }

    public void SpawnWardenOnButton(int index)
    {
        if (ZoneManager.Instance.moneyCount < wardensCost[index])
            return;
        Vector3 spawnPoint = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        var newWardenGameObject = Instantiate(wardenTypesPrefabs[index], spawnPoint, Quaternion.Euler(0, 180, 0));
        Instantiate(spawnParticles, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.identity);
        var newWarden = newWardenGameObject.GetComponent<WardenBase>();

        if (newWarden.transform.position.x > 0)
            newWarden.gameZone = GameZone.Right;
        else
            newWarden.gameZone = GameZone.Left;

        ZoneManager.Instance.AddWardenToList(newWarden);
        ZoneManager.Instance.moneyCount -= wardensCost[index];

        wardensCost[index] *= 2;
        wardensCostText[index].text = wardensCost[index].ToString() + " $";

        ZoneManager.Instance.UpdateMoneyCount();
    }

    public void SpawnSniperWardenOnButton()
    {
        if (ZoneManager.Instance.sniperWardens.Count > 1)
        {
            return;
        }

        if (ZoneManager.Instance.moneyCount < wardenSniperCost)
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
        ZoneManager.Instance.moneyCount -= wardenSniperCost;

        wardenSniperCost *= 2;
        wardenSniperCostText.text = wardenSniperCost.ToString() + " $";

        ZoneManager.Instance.UpdateMoneyCount();
    }
}