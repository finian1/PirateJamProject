using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PolygonCollider2D))]
public class BaseAttack : MonoBehaviour
{

    [Header("Settings")]
    public float essenceCost = 1.0f;
    public float attackDamage = 1.0f;
    public float cooldownTime = 1.0f;


    protected float cooldownTimer = 0.0f;
    public BaseWeapon weapon;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public virtual bool Attack()
    {
        if(cooldownTimer < cooldownTime)
        {
            return false;
        }
        cooldownTimer = 0.0f;

        weapon.player.currentCorruption -= essenceCost;
        return true;
    }
}
