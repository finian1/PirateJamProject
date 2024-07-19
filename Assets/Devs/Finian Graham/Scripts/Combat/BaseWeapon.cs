using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    [Header("Settings")]
    public BaseAttack[] attacks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack(int index)
    {
        attacks[index].Attack();
    }
}
