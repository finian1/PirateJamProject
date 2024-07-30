using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool paused;

    public GameObject screen;

    public void PauseGame(){

        screen.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void UnPause(){
        screen.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

            if(paused == true){
                UnPause();
            }
            else{
                PauseGame();
            }
        }
    }
}
