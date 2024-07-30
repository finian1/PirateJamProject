using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject enemy;
    
    public void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            enemy = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
            enemy.GetComponent<EnemyStateManager>().shouldPlaceSpawner = false;
        }
    }
}
