using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [NonSerialized]
    public CheckpointScript currentCheckpoint;
    [NonSerialized]
    public PlayerStateManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = (PlayerStateManager)FindFirstObjectByType(typeof(PlayerStateManager));
    }

    public void RespawnPlayer()
    {
        //TODO: If not checkpoint, restart level.
        if (currentCheckpoint == null) return;

        player.transform.position = currentCheckpoint.spawnPoint.position;

        EnemyStateManager[] aliveEnemies;
        aliveEnemies = FindObjectsByType<EnemyStateManager>(FindObjectsSortMode.None);
        EnemySpawnPoint[] spawnPoints;
        spawnPoints = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);

        foreach (EnemyStateManager enemy in aliveEnemies)
        {
            Destroy(enemy.gameObject);
        }
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }

        //Player reset stats
        player.currentHealth = player.initialHealth;
        player.currentCorruption = currentCheckpoint.corruptionValue;

    }
}
