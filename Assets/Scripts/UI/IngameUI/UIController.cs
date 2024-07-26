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
    [Range(0,100)]
    public float barSpeed = 2;

    

    [Header("Corruption")]
    [Range(0,100)]
    public float Corruption;
     [Range(0,100)]
    public float minCorruption;
    [Range(0,100)]
    public float maxCorruption;

       public Image corruptionFill;
         [Range(0,100)]
    public float bSpeed = 2;


    void awake()
    {
        controller = this;
    }


   
    void Start()
    {
        currentHealth = 100;
     
        maxHealth = 100;

        Corruption = 100;
        maxCorruption = 100;
    }

    void Update()
    {
        float fillAmount = currentHealth / maxHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);

        float fillAmount2 = Corruption / maxCorruption;
        healthFill.fillAmount = Mathf.Lerp(corruptionFill.fillAmount, fillAmount, Time.deltaTime * bSpeed);
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


       public void LooseCorruption(float damage)
    {
        Corruption -= damage;
        if (Corruption < 0) Corruption = 0;
    }

    public void GainCorruption(float healAmount)
    {
        Corruption += healAmount;
        if (Corruption > maxCorruption) Corruption = maxCorruption;
    }
}

