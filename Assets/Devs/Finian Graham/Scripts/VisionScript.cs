using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionScript : MonoBehaviour
{
    public bool canSeePlayer = false;

    public GameObject playerObject;
    private bool isPlayerPresent;

    private void FixedUpdate()
    {
        if (isPlayerPresent)
        {
            Vector2 targetOffset = playerObject.transform.position - transform.position;
            RaycastHit2D visionCheckHit;
            int layerMask = 1 << 3;
            if (visionCheckHit = Physics2D.Raycast(gameObject.transform.position, targetOffset.normalized, targetOffset.magnitude, layerMask))
            {
                canSeePlayer = false;
            }
            else
            {
                canSeePlayer = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerPresent = true;
            playerObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerPresent = false;
            canSeePlayer = false;
            playerObject = null;
        }
    }

    public bool IsPlayerInFront(Vector3 forwardVector)
    {
        if(playerObject == null)
        {
            return false;
        }

        Vector3 targetOffset = playerObject.transform.position - transform.position;
        targetOffset.Normalize();
        if(Vector3.Dot(forwardVector, targetOffset) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
