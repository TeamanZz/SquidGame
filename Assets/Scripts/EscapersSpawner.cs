using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapersSpawner : MonoBehaviour
{
    public GameObject escaperPrefab;
    public GameZone gameZone;

    public float minSpawnTime = 1;
    public float maxSpawnTime = 5;

    private void Start()
    {
        StartCoroutine(SpawnEscapersRepeatedely());
    }

    private IEnumerator SpawnEscapersRepeatedely()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1.8f, 1.8f), 0.1f, transform.position.z);
            var newEscaper = Instantiate(escaperPrefab, spawnPosition, Quaternion.identity);
            var escaperComponent = newEscaper.GetComponent<Escaper>();
            escaperComponent.gameZone = gameZone;
            ZoneManager.Instance.AddEscaperToList(escaperComponent, gameZone);
            maxSpawnTime -= 0.01f;
        }
    }
}