using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PolygonCollider2D))]
public class AOEAttack : BaseAttack
{
    PolygonCollider2D areaOfEffect;
    // Start is called before the first frame update
    void Start()
    {
        areaOfEffect = GetComponent<PolygonCollider2D>();
    }

    public override void Attack()
    {
        if(cooldownTimer < cooldownTime)
        {
            return;
        }

        base.Attack();
        List<Collider2D> objectsToAttack = new List<Collider2D>();
        areaOfEffect.Overlap(objectsToAttack);
        
        foreach(Collider2D target in objectsToAttack)
        {
            ExecuteEvents.Execute<IInteractionEvents>(target.gameObject, null, (message, data) => message.Damage(attackDamage, gameObject));
        }
    }
}
