using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractableObject
{
    private bool isOpen = false;
    public Collider2D doorCollider;

    public Sprite closedSprite;
    public Sprite openSprite;

    public void Interact(GameObject origin)
    {

        isOpen = !isOpen;
        if(isOpen)
        {
            doorCollider.enabled = false;
            doorCollider.gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            doorCollider.enabled = true;
            doorCollider.gameObject.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
}
