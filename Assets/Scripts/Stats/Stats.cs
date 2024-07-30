using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats instance;

    [Header("Player Stats")]
    public float initialHealth ;
    public float currentHealth ;
    public float initialCorruption ;
    public float currentCorruption ;


    void Awake(){
        instance = this;

    }

    void Start()
    {
        if(PlayerPrefs.HasKey("Health") &&PlayerPrefs.HasKey("Corruption")) {
            currentHealth = PlayerPrefs.GetFloat("Health");
            currentCorruption = PlayerPrefs.GetFloat("Corruption");
            Debug.Log("yes");

            
        }
        else{
        currentHealth = 100;
        currentCorruption = 100;
        Debug.Log("no");

        }
       
    }

    void Update()
    {
        
    }
}
