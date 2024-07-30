using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioOption : MonoBehaviour
{
  
    [Header("Mixer")]
    [SerializeField] AudioMixer mixer;

    public Slider musicSlider;
    public Slider sfxSlider;

     void Start(){
        LoadVolume();
     }

    public void setMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", value);
    }
  
    
    public void setSFXcVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }
    
   public void SaveVolume()
   {
    mixer.GetFloat("MusicVolume", out float MusicVolume);
    PlayerPrefs.SetFloat("MusicVolume",MusicVolume);

    mixer.GetFloat("SFXVolume", out float SFXVolume);
    PlayerPrefs.SetFloat("SFXVolume",SFXVolume);
   }
   public void LoadVolume()
   {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

   }
   
 
}
