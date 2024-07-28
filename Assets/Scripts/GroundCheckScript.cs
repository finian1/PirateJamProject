using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public LayerMask groundLayer;

    public bool IsGroundPresent(string friendlyTag)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(colliders);

        foreach (Collider2D collider in colliders)
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
