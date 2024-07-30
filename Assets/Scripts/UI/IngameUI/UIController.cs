using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController controller;

    [Header("Health")]
    public Image healthFill;
    [Range(0,100)]
    public float barSpeed = 2;


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
        float fillAmount = Stats.currentHealth /Stats.initialHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);

        float fillAmount2 = Stats.currentCorruption /Stats.initialCorruption;
        corruptionFill.fillAmount = Mathf.Lerp(corruptionFill.fillAmount, fillAmount2, Time.deltaTime * bSpeed);
    }


}

