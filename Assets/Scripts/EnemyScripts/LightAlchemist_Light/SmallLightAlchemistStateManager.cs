using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmallLightAlchemistStateManager : EnemyStateManager
{
    [Header("Small Alchemist Settings")]
    public float maxInvestigationTime = 5.0f;
    public float minInvestigationTime = 1.0f;
    public float maxTimeBetweenInvestigation = 5.0f;
    public float minTimeBetweenInvestigation = 1.0f;
    public float timeBetweenViewFlips = 0.5f;

    public override void Awake()
    {
        EnemyStates[EnemyState.ROAMING] = new SmallLightAlchemistRoamingState();
        EnemyStates[EnemyState.INVESTIGATING] = new SmallLightAlchemistInvestigatingState();

        base.Awake();
    }


    public void Attack()
    {
        Debug.DrawLine(transform.position, transform.position + forwardVector * attackRange, Color.cyan, 1.0f);
        List<Collider2D> targets = new List<Collider2D>();
        attackArea.Overlap(targets);

        foreach (Collider2D target in targets)
        {
            ExecuteEvents.Execute<IDamageableObject>(target.gameObject, null, (message, data) => message.Damage(attackDamage, gameObject));
        }
    }
}
