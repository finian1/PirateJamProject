using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public menuScreens menuScreens;

    public GameObject optionsscr;
    public GameObject controlscrn;
    
    public void mainmenu(){
        menuScreens = menuScreens.main;

    }

    public void controlls(){
        menuScreens = menuScreens.controls;

    }

    public void options(){
        menuScreens = menuScreens.settings;

    }

    public void play(){

       Stats.currentCorruption = Stats.initialCorruption;
       Stats.currentHealth = Stats.initialHealth;

    }

    public void credit(){
        menuScreens = menuScreens.credits;

    }



    void Update()
    {


    switch (menuScreens)
    {
    case menuScreens.main:
    Debug.Log(menuScreens);
    controlscrn.SetActive(false);
    break;
    case menuScreens.settings:
    Debug.Log(menuScreens);
    controlscrn.SetActive(false);
    break;
    case menuScreens.controls:
    Debug.Log(menuScreens);
    controlscrn.SetActive(true);
    break;
    case menuScreens.credits:
    Debug.Log(menuScreens);
    controlscrn.SetActive(false);
   break;
    }

    }




}


public enum menuScreens
{
    main,
    settings,
    controls,

    credits
}
