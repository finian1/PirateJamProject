using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public Sprite openGate;
    public Sprite closedGate;

    public void OpenGate()
    {
        GetComponent<SpriteRenderer>().sprite = openGate;
        GetComponent<Collider2D>().enabled = false;
    }

    public void CloseGate()
    {
        GetComponent<SpriteRenderer>().sprite = closedGate;
        GetComponent<Collider2D>().enabled = true;
    }
}
