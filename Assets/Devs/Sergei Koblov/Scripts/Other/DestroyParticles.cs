using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{

    [SerializeField] private float particleTimer;
    [SerializeField] private float timeUntilDestruction;

    private void Update()
    {
        particleTimer += Time.deltaTime;

        if (particleTimer > timeUntilDestruction)
        {
            Destroy(gameObject);
        }
    }

}
