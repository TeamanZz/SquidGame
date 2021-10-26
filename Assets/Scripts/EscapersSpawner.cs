using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapersSpawner : MonoBehaviour
{
    public List<GameObject> escapers = new List<GameObject>();

    public int escapersRemaining;

    [Header("Шансы спавна")]
    public int heavyEscaperSpawnChance;
    public int crowdSpawnChance;

    [Header("Размер толпы")]
    public int minCrowdCount;
    public int maxCrowdCount;

    [Header("Промежуток между спавном")]
    public float minSpawnTime = 1;
    public float maxSpawnTime = 5;

    private float minXOffsetLeft = -3;
    private float maxXOffsetLeft = 0.5f;
    private float minXOffsetRight = -0.5f;
    private float maxXOffsetRight = 1;


    [Space]

    public Transform leftSpawnGate;
    public Transform rightSpawnGate;

    public TextMeshProUGUI escapersText;


    private void Start()
    {
        escapersText.text = escapersRemaining.ToString();
        StartCoroutine(SpawnEscapersRepeatedely());
    }

    private IEnumerator SpawnEscapersRepeatedely()
    {
        while (escapersRemaining > 0)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            GameZone gameZone = (GameZone)Random.Range(0, 3);
            Vector3 spawnPosition = SetSpawnPosition(gameZone);
            int needSpawnCrowd = Random.Range(0, crowdSpawnChance + 1);

            if (needSpawnCrowd == 0)
            {
                int crowdCount = 1;

                if (maxCrowdCount < escapersRemaining)
                    crowdCount = Random.Range(minCrowdCount, maxCrowdCount);
                else
                    crowdCount = Random.Range(minCrowdCount, escapersRemaining);

                for (int i = 0; i < crowdCount; i++)
                {
                    int crowdEnemyType = SetEnemyType();
                    var newEscaper = Instantiate(escapers[crowdEnemyType], spawnPosition, Quaternion.identity);
                    var escaperComponent = newEscaper.GetComponent<Escaper>();
                    escaperComponent.gameZone = gameZone;
                    if (escapersRemaining == 1)
                        escaperComponent.isLastEnemy = true;
                    ZoneManager.Instance.AddEscaperToList(escaperComponent, gameZone);
                    SetNewEscapersTextValue();
                    if (escapersRemaining <= 0)
                        break;
                }
            }
            else
            {
                int enemyType = SetEnemyType();
                var newEscaper = Instantiate(escapers[enemyType], spawnPosition, Quaternion.identity);
                var escaperComponent = newEscaper.GetComponent<Escaper>();
                escaperComponent.gameZone = gameZone;
                if (escapersRemaining == 1)
                    escaperComponent.isLastEnemy = true;
                ZoneManager.Instance.AddEscaperToList(escaperComponent, gameZone);
                SetNewEscapersTextValue();
            }

            maxSpawnTime -= 0.01f;
        }
    }

    private void SetNewEscapersTextValue()
    {
        escapersRemaining--;
        escapersText.text = escapersRemaining.ToString();
    }

    private Vector3 SetSpawnPosition(GameZone gameZone)
    {
        Vector3 spawnPosition = new Vector3();
        if (gameZone == GameZone.Left)
            spawnPosition = new Vector3(leftSpawnGate.position.x + Random.Range(minXOffsetLeft, maxXOffsetLeft), 0, leftSpawnGate.position.z);
        else
            spawnPosition = new Vector3(rightSpawnGate.position.x + Random.Range(minXOffsetRight, maxXOffsetRight), 0, rightSpawnGate.position.z);
        return spawnPosition;
    }

    private int SetEnemyType()
    {
        int enemyType = 0;
        int needSpawnHeavy = Random.Range(0, heavyEscaperSpawnChance + 1);
        if (needSpawnHeavy == 0)
            enemyType = 1;
        return enemyType;
    }
}