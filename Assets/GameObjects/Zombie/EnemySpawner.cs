using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numberOfEnemies = 1;
    public int enemiesRemaining = 0;

    public override void OnStartServer()
    {
        SpawnNewWave(numberOfEnemies);
    }

    public void SpawnNewWave(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
			Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-8.0f, 8.0f), transform.position.y, transform.position.z + Random.Range(-8.0f, 8.0f));
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
            enemiesRemaining++;
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;

        if(enemiesRemaining == 0)
        {
            numberOfEnemies++;
            SpawnNewWave(numberOfEnemies);
        }
    }

}