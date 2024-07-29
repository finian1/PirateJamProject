using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptScript : MonoBehaviour
{
    public SpriteRenderer prompt;


    private void Start()
    {
        if (prompt != null)
        {
            prompt.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (prompt == null) return;

        if (collision.CompareTag("Player"))
        {
            prompt.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (prompt == null) return;

        if (collision.CompareTag("Player"))
        {
            prompt.enabled = false;
        }
    }
}
