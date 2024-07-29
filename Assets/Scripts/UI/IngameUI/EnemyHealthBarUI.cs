using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    private Camera _cam;

    void Start(){
        _cam = Camera.main;
    }

    void Update(){
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    
    public void HealthBar(float maxhealth, float currenthealth)
    {
        _healthbarSprite.fillAmount = currenthealth/maxhealth;
    }

    
}
