using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : BaseAttack
{

    public GameObject projectile;
    
    public override bool Attack()
    {
        if(!base.Attack())
        {
            return false;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        GameObject projectileObject = Instantiate(projectile, weapon.transform.position, weapon.transform.rotation);
        projectileObject.transform.right = mousePos - new Vector2(weapon.transform.position.x, weapon.transform.position.y);
        projectileObject.GetComponent<Projectile>().damageToDeal = attackDamage;

        return true;
    }
}
