using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    private PlayerStateManager player;

    public AudioClip[] normalClips;
    public AudioClip[] corruptedClips;

    private bool shouldPlayNextTrack = true;
    public float corruptionForEvilTracks = 50.0f;
    private bool playingCorruptedTracks = false;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        player = FindFirstObjectByType<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldPlayNextTrack)
        {
            if(Stats.currentCorruption <= 50.0f)
            {
                int randomClip = Random.Range(0, corruptedClips.Length);
                StartCoroutine(PlayClip(corruptedClips[randomClip].length, corruptedClips[randomClip]));
            }
            else
            {
                int randomClip = Random.Range(0, normalClips.Length);
                Coroutine clip = StartCoroutine(PlayClip(normalClips[randomClip].length, normalClips[randomClip]));
            }
            shouldPlayNextTrack=false;
        }
    }


    public IEnumerator PlayClip(float time, AudioClip clip)
    {
        musicSource.time = 0.0f;
        musicSource.clip = clip;
        musicSource.Play();
        float timer = 0.0f;
        while(timer <= time + 1.0f)
        {
            yield return new WaitForSeconds(1.0f);
            timer += 1.0f;
            if(Stats.currentCorruption <= 50.0f && !playingCorruptedTracks)
            {
                
                musicSource.Pause();
                shouldPlayNextTrack = true;
                playingCorruptedTracks = true;
                yield break;
            }
        }
        musicSource.Pause();
        shouldPlayNextTrack = true;
        yield return null;
    }
}
