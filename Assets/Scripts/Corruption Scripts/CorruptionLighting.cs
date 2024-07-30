using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CorruptionLighting : MonoBehaviour
{

    public PlayerStateManager player;
    public float minimumLightingIntensity = 0.01f;

    private Light2D lighting;
    private float initLightLevel;

    // Start is called before the first frame update
    void Start()
    {
        lighting = GetComponent<Light2D>();
        initLightLevel = lighting.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        lighting.intensity = Mathf.Clamp(initLightLevel * (Stats.currentCorruption / 100.0f), minimumLightingIntensity, 1.0f);
    }
}
