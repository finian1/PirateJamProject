using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
