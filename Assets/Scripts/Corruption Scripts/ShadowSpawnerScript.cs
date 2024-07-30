using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawnerScript : MonoBehaviour
{
    public PlayerStateManager player;
    public float corruptionRequiredForSpawn = 90.0f;
    public GameObject shadowToSpawn;
    public GameObject spawnPosition;

    private GameObject spawnedShadow;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasSpawned && collision.CompareTag("Player") && Stats.instance.currentCorruption <= corruptionRequiredForSpawn)
        {
            spawnedShadow = Instantiate(shadowToSpawn, spawnPosition.transform.position, spawnPosition.transform.rotation);
            hasSpawned = true;
        }
    }

    public void ResetSpawn()
    {
        if (hasSpawned)
        {
            hasSpawned = false;
            Destroy(spawnedShadow);
        }
    }

}
