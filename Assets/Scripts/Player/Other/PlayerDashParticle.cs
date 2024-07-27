using UnityEngine;

public class PlayerDashParticle : MonoBehaviour
{
    public GameObject mainPlayer;
    public ParticleSystem pSystem;
    public float dashParticleTimer;
    public float dashParticleDuration = 0.1f;
    public bool dashParticlesPlaying = false;
    public Material dashMaterial;
    public Material dashMaterialFlipped;

    private void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        var emission = pSystem.emission;
        emission.enabled = false;
        
    }

    private void Update()
    {
        if(mainPlayer.transform.localScale.x == 1f)
        {
            pSystem.GetComponent<Renderer>().material = dashMaterial;
        }
        if(mainPlayer.transform.localScale.x == -1f)
        {
            pSystem.GetComponent<Renderer>().material = dashMaterialFlipped;
        }

        PlayParticles();
    }

    public void PlayParticles()
    {
        if(dashParticlesPlaying)
        {
            var emission = pSystem.emission;
            dashParticleTimer += Time.deltaTime;
            
            if(dashParticleTimer < dashParticleDuration)
            {
                emission.enabled = true;
            }

            if(dashParticleTimer > dashParticleDuration)
            {
                emission.enabled = false;
                dashParticleTimer = 0f;
                dashParticlesPlaying = false;
            }
        }
    }
}
