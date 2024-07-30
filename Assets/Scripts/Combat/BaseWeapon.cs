using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    [Header("Settings")]
    public BaseAttack[] attacks;

    public PlayerStateManager player;

   

    // Start is called before the first frame update
    void Start()
    {
        foreach(BaseAttack attack in attacks)
        {
            attack.weapon = this;
        }
    }

    public void Attack(int index)
    {
        attacks[index].Attack();
    }
}
