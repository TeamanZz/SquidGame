using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapersSpawner : MonoBehaviour
{
    public GameObject escaperPrefab;

    public float minSpawnTime = 1;
    public float maxSpawnTime = 5;

    public Transform leftSpawnGate;
    public Transform rightSpawnGate;

    public float minXOffsetLeft;
    public float maxXOffsetLeft;

    public float minXOffsetRight;
    public float maxXOffsetRight;

    public int escapersRemaining;
    public TextMeshProUGUI escapersText;

    private void Start()
    {
        StartCoroutine(SpawnEscapersRepeatedely());
    }

    private IEnumerator SpawnEscapersRepeatedely()
    {
        while (escapersRemaining > 0)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            GameZone gameZone = (GameZone)Random.Range(0, 2);

            Vector3 spawnPosition = new Vector3();
            if (gameZone == GameZone.Left)
                spawnPosition = new Vector3(leftSpawnGate.position.x + Random.Range(minXOffsetLeft, maxXOffsetLeft), 0, leftSpawnGate.position.z);
            else
                spawnPosition = new Vector3(rightSpawnGate.position.x + Random.Range(minXOffsetRight, maxXOffsetRight), 0, rightSpawnGate.position.z);

            var newEscaper = Instantiate(escaperPrefab, spawnPosition, Quaternion.identity);
            var escaperComponent = newEscaper.GetComponent<Escaper>();
            escaperComponent.gameZone = gameZone;
            ZoneManager.Instance.AddEscaperToList(escaperComponent, gameZone);
            maxSpawnTime -= 0.01f;
            escapersRemaining--;
            escapersText.text = escapersRemaining.ToString();

        }
    }
}