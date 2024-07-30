using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject[] popUps;
    private int popupIndex;
    void Update()
    {

        for(int i = 0; i < popUps.Length; i++){
            if(i == popupIndex){
                popUps[popupIndex].SetActive(true);
            }
            else{
                popUps[popupIndex].SetActive(false);
            }
        }

        if(popupIndex ==0){
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D)){

                popupIndex++;

            }
            else if(popupIndex == 1){
                
            }
        }


        
    }
}
