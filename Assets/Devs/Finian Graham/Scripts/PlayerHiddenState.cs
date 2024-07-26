using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiddenState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().Sleep();
        player.isHidden = true;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<Collider2D>().enabled = true;
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<Rigidbody2D>().WakeUp();
            player.isHidden = false;
            player.unhiding = true;
            player.SwitchState(PlayerState.IDLE);
        }
    }
}
