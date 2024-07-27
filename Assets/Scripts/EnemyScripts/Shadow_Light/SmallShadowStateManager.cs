using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShadowStateManager : EnemyStateManager
{
    public GameObject player;
    //Amount of time to roam before hiding again.
    public float timeBeforeHiding = 5.0f;
    //Amount of time to spot player before coming out of hiding.
    public float timeBeforeAmbush = 1.0f;

    public float lungeDistance = 1.0f;

    // Start is called before the first frame update
    public override void Start()
    {
        EnemyStates[EnemyState.HIDING] = new ShadowHiddenState();
        EnemyStates[EnemyState.ROAMING] = new ShadowRoamingState();
        EnemyStates[EnemyState.ATTACKING] = new ShadowAttackState();

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
