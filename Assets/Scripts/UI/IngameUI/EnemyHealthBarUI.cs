using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    public Image healthFill;
    [Range(0,100)]
    public float barSpeed = 2;

    public EnemyStateManager etm;
    void Update(){
    float fillAmount = etm.currentEnemyHealth/etm.initialEnemyHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);
    }

    
}
