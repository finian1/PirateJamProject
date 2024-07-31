using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class SmallShadowStateManager : EnemyStateManager
{
    public GameObject player;
    //Amount of time to roam before hiding again.
    public float timeBeforeHiding = 5.0f;
    //Amount of time to spot player before coming out of hiding.
    public float timeBeforeAmbush = 1.0f;

    public float lungeDistance = 1.0f;

    public float timeBetweenBodyDamage = 0.1f;
    private float bodyDamageTimer = 0.0f;

    public float timeToSpawn = 1.0f;

    public override void Awake()
    {
        EnemyStates[EnemyState.HIDING] = new ShadowHiddenState();
        EnemyStates[EnemyState.ROAMING] = new ShadowRoamingState();
        EnemyStates[EnemyState.ATTACKING] = new ShadowAttackState();
        EnemyStates[EnemyState.SPAWNING] = new ShadowSpawningState();
        base.Awake();
        SwitchState(EnemyState.SPAWNING);
    }

    public override void FixedUpdate()
    {
        bodyDamageTimer += Time.deltaTime;
        base.FixedUpdate();
    }

    public override void Damage(float amount, GameObject source)
    {
        //Don't allow shadows to be damaged by the player
        if(!source.CompareTag("Player"))
        {
            base.Damage(amount, source);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExecuteEvents.Execute<IDamageableObject>(collision.gameObject, null, (message, data) => message.Damage(attackDamage / 2.0f, gameObject));
        }
    }
}
