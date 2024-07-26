using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpotlight : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // can't go beyond screen bounds
        Cursor.visible = false; // cursor not visible
    }
   
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // set's mouse position
        transform.LookAt(mouseRay.origin + mouseRay.direction); // moves spotlight with mouse cursor position
    }
}
