using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController controller;

    [Header("Health")]
    public Image healthFill;
    [Range(0,100)]
    public float barSpeed = 2;

    PlayerStateManager stm;

    [Header("Corruption")]
    public Image corruptionFill;
     [Range(0,100)]
    public float bSpeed = 2;


    void awake()
    {
        controller = this;
    }

    void Update()
    {
        float fillAmount = stm.currentHealth / stm.initialHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);

        float fillAmount2 = stm.currentCorruption /stm.initialCorruption;
        corruptionFill.fillAmount = Mathf.Lerp(corruptionFill.fillAmount, fillAmount2, Time.deltaTime * bSpeed);
    }

    public void TakeDamage(float damage)
    {
        stm.currentHealth -= damage;
        if (stm.currentHealth < 0) stm.currentHealth = 0;
    }

    public void Heal(float healAmount)
    {
        stm.currentHealth += healAmount;
        if (stm.currentHealth > stm.initialHealth) stm.currentHealth = stm.initialHealth;
    }


       public void LooseCorruption(float damage)
    {
        stm.currentCorruption -= damage;
        if (stm.currentCorruption < 0)  stm.currentCorruption = 0;
    }

    public void GainCorruption(float healAmount)
    {
         stm.currentCorruption += healAmount;
        if ( stm.currentCorruption > stm.initialCorruption)  stm.currentCorruption = stm.initialCorruption;
    }
}

