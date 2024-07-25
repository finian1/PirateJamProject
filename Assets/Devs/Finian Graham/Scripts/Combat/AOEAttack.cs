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

    public override bool Attack()
    {
        if (!base.Attack())
        {
            return false;
        }

        List<Collider2D> objectsToAttack = new List<Collider2D>();
        areaOfEffect.Overlap(objectsToAttack);
        
        foreach(Collider2D target in objectsToAttack)
        {
            ExecuteEvents.Execute<IDamageableObject>(target.gameObject, null, (message, data) => message.Damage(attackDamage, gameObject));
        }
        return true;
    }
}
