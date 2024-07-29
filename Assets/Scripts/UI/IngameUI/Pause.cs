using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Pause : MonoBehaviour
{

    public bool pause;

    public GameObject pauseMenu;

   public void Paused()
   {
    pauseMenu.SetActive(true);
    Time.timeScale = 0;
    pause = true;

   }

   public void UnPaused()
   {
     pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pause = false;
   }

   void Update(){

    if(Input.GetKeyDown(KeyCode.Escape)){

        Paused();

        
    }


   }

}
