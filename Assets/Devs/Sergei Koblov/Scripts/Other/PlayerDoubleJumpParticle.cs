using UnityEngine;

public class PlayerDoubleJumpParticle : MonoBehaviour
{
    public ParticleSystem pSystem;
    public float doubleJumpParticleTimer;
    public float doubleJumpParticleDuration = 0.5f;
    public bool doubleJumpParticlesPlaying = false;
    public bool emittedOnce = false;

    private void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        var emission = pSystem.emission;
        emission.enabled = false;
    }

    private void Update()
    {
        PlayParticles();
    }

    public void PlayParticles()
    {
        if (doubleJumpParticlesPlaying)
        {
            var emission = pSystem.emission;
            doubleJumpParticleTimer += Time.deltaTime;

            if (doubleJumpParticleTimer < doubleJumpParticleDuration)
            {
                emission.enabled = true;
                if(!emittedOnce)
                {
                    pSystem.Emit(1);
                    pSystem.Play();
                    emittedOnce = true;
                }
            }

            if (doubleJumpParticleTimer > doubleJumpParticleDuration)
            {
                emission.enabled = false;
                doubleJumpParticleTimer = 0f;
                doubleJumpParticlesPlaying = false;
                emittedOnce = false;
            }
        }
    }
}
