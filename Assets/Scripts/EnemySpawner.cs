using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDelay;
    public float startForce;
    public Transform[] spawnPoints;
    [Space]
    public GameObject enemyPrefab;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SpawnEnemies()
    {
        StartCoroutine(spawnEnum());
    }

    public void SpawnRandomEnemy()
    {
        SpawnEnemy(spawnPoints[Random.Range(0, 4)]);
    }

    public void SpawnEnemy(Transform sp)
    {
        GameObject go = Instantiate(enemyPrefab, sp.position, Quaternion.identity);

        go.GetComponent<Rigidbody2D>().velocity = sp.right * Random.Range(startForce / 2, startForce);
    }

    IEnumerator spawnEnum()
    {
        for (int i = 0; i < GameManager.currWave; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            SpawnEnemy(spawnPoints[i % 4]);
        }
    }
}
