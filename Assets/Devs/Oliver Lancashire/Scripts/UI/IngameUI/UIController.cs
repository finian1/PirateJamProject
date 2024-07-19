using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController controller;

    [Header("Health")]
    [Range(0,100)]
    public float currentHealth;
    [Range(0,100)]
    public float maxHealth;
    [Range(0,100)]
    public float minHealth;
    public Image healthFill;
    public float barSpeed = 2;

    

    [Header("Corruption")]
    [Range(0,100)]
    public float Corruption;

    public float minCorruption;

    public float maxCorruption;


    void awake()
    {
        controller = this;
    }


   
    void Start()
    {
        currentHealth = 100;
     
        maxHealth = 100;
    }

    void Update()
    {
        float fillAmount = currentHealth / maxHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}

