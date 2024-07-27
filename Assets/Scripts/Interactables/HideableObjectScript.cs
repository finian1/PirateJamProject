using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObjectScript : MonoBehaviour, IInteractableObject
{
    public Sprite hiddenSprite;
    public Sprite unhiddenSprite;

    public void Interact(GameObject origin)
    {
        PlayerStateManager player = origin.GetComponent<PlayerStateManager>();
        if(player != null)
        {
            GetComponent<SpriteRenderer>().sprite = hiddenSprite;
            player.SwitchState(PlayerState.HIDDEN);
            player.currentHidingPlace = gameObject;
        }
    }

    public void Unhide()
    {
        GetComponent<SpriteRenderer>().sprite = unhiddenSprite;
    }
}
