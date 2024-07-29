using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject controlls_screen;

    public GameObject credit_screen;
    public menuScreens menuScreens;

    
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
    controlls_screen.SetActive(false);
    break;
    case menuScreens.settings:
    Debug.Log(menuScreens);
     controlls_screen.SetActive(false);
    break;
    case menuScreens.controls:
    Debug.Log(menuScreens);
    controlls_screen.SetActive(true);
    break;
    case menuScreens.credits:
    Debug.Log(menuScreens);
    controlls_screen.SetActive(false);
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
