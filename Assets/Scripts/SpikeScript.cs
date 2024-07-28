using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class SpikeScript : MonoBehaviour
{
    public float spikeDamage = 10.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExecuteEvents.Execute<IDamageableObject>(collision.gameObject, null, (message, data) => message.Damage(spikeDamage, /*weapon.player.*/gameObject));
    }
}
