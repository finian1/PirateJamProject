using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject litLight;
    RespawnManager respawnManager;
    bool triggered = false;

    public SpriteRenderer checkpointSprite;
    public Sprite untriggeredSprite;
    public Sprite triggeredSprite;

    //Player stats to reset to
    [NonSerialized]
    public float corruptionValue;

    private void Start()
    {
        respawnManager = (RespawnManager)FindFirstObjectByType(typeof(RespawnManager));
        checkpointSprite.sprite = untriggeredSprite;
        litLight.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (respawnManager == null) return;

        PlayerStateManager player;
        if (!triggered && collision.TryGetComponent<PlayerStateManager>(out player))
        {
            triggered = true;
            checkpointSprite.sprite = triggeredSprite;
            corruptionValue = Stats.currentCorruption;
            litLight.SetActive(true);
            respawnManager.currentCheckpoint = this;
        }
    }



}
