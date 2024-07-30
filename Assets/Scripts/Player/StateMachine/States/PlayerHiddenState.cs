using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiddenState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Collider2D[] colliders = player.GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().Sleep();
        player.isHidden = true;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliders = player.GetComponents<Collider2D>();
            foreach(Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().WakeUp();
            player.isHidden = false;
            player.unhiding = true;
            player.currentHidingPlace.GetComponent<HideableObjectScript>().Unhide();
            player.SwitchState(PlayerState.IDLE);
        }
    }
}
