using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] potentialClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int randomVal = Random.Range(0, potentialClips.Length);

        audioSource.time = 0.0f;
        audioSource.clip = potentialClips[randomVal];
        audioSource.Play();
    }

}
