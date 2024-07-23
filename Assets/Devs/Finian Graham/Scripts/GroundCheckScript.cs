using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public LayerMask groundLayer;

    public bool IsGroundPresent()
    {
        if (Physics2D.OverlapCircle(transform.position, GetComponent<CircleCollider2D>().radius, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
