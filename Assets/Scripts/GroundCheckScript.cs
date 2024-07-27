using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public LayerMask groundLayer;

    public bool IsGroundPresent(string friendlyTag)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground") ||
                collider.gameObject.CompareTag(friendlyTag) ||
                collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;

    }
}
