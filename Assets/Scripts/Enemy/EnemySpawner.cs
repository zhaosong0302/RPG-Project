using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabEnemy;
    public float spawnTime = 10;

    float spawnTimer;

    void Start()
    {
        SpawnEnery();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnTime)
        {
            spawnTimer = 0;
            SpawnEnery();
        }
    }

    void SpawnEnery()
    {
        GameObject.Instantiate(prefabEnemy, transform.position, Quaternion.identity);
    }
}
