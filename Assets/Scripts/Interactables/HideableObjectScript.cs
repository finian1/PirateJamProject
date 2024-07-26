using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObjectScript : MonoBehaviour, IInteractableObject
{
    public void Interact(GameObject origin)
    {
        PlayerStateManager player = origin.GetComponent<PlayerStateManager>();
        if(player != null)
        {
            player.SwitchState(PlayerState.HIDDEN);
        }
    }
}
