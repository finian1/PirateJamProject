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

    public float timeToRevealFor = 4.0f;

    private float prevDisplayedHealth;
    private float revealTimer = 0.0f;

    public EnemyStateManager etm;

    private void Start()
    {
        GetComponent<Canvas>().enabled = false;
        prevDisplayedHealth = etm.currentEnemyHealth;
    }

    void Update(){

        float fillAmount = etm.currentEnemyHealth/etm.initialEnemyHealth;
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, fillAmount, Time.deltaTime * barSpeed);

        if (prevDisplayedHealth != etm.currentEnemyHealth)
        {
            if (!GetComponent<Canvas>().enabled)
            {
                revealTimer = timeToRevealFor;
                StartCoroutine(ShowHealthbar());
            }
            else
            {
                revealTimer = timeToRevealFor;
            }
        }

        prevDisplayedHealth = etm.currentEnemyHealth;
    }

    public IEnumerator ShowHealthbar()
    {
        GetComponent<Canvas>().enabled = true;
        while (revealTimer > 0.0f)
        {
            yield return new WaitForSeconds(1f);
            revealTimer -= 1.0f;
        }
        GetComponent<Canvas>().enabled = false;
    }

    
}
