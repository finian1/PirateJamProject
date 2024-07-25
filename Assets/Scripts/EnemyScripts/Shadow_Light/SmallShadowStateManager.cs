using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShadowStateManager : EnemyStateManager, IInteractionEvents
{
    public GameObject player;

    // Start is called before the first frame update
    new void Start()
    {
        EnemyStates[EnemyState.HIDING] = new ShadowHiddenState();
        EnemyStates[EnemyState.ROAMING] = new ShadowRoamingState();

        base.Start();
    }

    public override void Damage(float amount, GameObject source)
    {
        //Don't allow shadows to be damaged by the player
        if(!source.CompareTag("Player"))
        {
            base.Damage(amount, source);
        }
    }
}
