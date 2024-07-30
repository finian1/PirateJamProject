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

        //Player reset stats
        Stats.currentHealth = Stats.initialHealth;
        Stats.currentCorruption = currentCheckpoint.corruptionValue;

        player.transform.position = currentCheckpoint.spawnPoint.position;

        EnemyStateManager[] aliveEnemies;
        aliveEnemies = FindObjectsByType<EnemyStateManager>(FindObjectsSortMode.None);
        EnemySpawnPoint[] spawnPoints;
        spawnPoints = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
        ShadowSpawnerScript[] shadowSpawners;
        shadowSpawners = FindObjectsByType<ShadowSpawnerScript>(FindObjectsSortMode.None);

        foreach (EnemyStateManager enemy in aliveEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }
        foreach(ShadowSpawnerScript shadowSpawner in shadowSpawners)
        {
            shadowSpawner.ResetSpawn();
        }



    }
}
