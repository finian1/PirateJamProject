using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Drawing;

public class ButtonHovering : MonoBehaviour
{
   public TextMeshProUGUI tm;
    void Start(){

                                          //   R    G   B   A
    tm.faceColor =  new Color32(255, 255, 255, 50);

    }

   public void onHover(TextMeshProUGUI meshProUGUI)
   {
                                        //   R    G   B   A
     meshProUGUI.faceColor =  new Color32(255, 255, 255, 255);

   }

   public void offHover(TextMeshProUGUI meshProUGUI)
   {
                                     //   R    G   B   A
    meshProUGUI.faceColor =  new Color32(255, 255, 255, 50);
   }

   
}
