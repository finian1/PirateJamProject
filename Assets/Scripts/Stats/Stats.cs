using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats instance;

    [Header("Player Stats")]
    public float initialHealth = 100.0f;
    public float currentHealth ;
    public float initialCorruption = 100.0f;
    public float currentCorruption ;


    void Awake(){
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 50;
        currentCorruption = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
