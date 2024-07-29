using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    [SerializeField] private float gameObjectTimer;
    [SerializeField] private float timeUntilDestruction;

    private void Update()
    {
        gameObjectTimer += Time.deltaTime;

        if (gameObjectTimer > timeUntilDestruction)
        {
            Destroy(gameObject);
        }
    }
}
