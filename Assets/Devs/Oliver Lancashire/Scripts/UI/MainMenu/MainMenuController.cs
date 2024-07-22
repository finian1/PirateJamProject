using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

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
    break;
    case menuScreens.settings:
    Debug.Log(menuScreens);
    break;
    case menuScreens.controls:
    Debug.Log(menuScreens);
    break;
    case menuScreens.credits:
    Debug.Log(menuScreens);
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
