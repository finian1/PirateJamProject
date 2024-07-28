using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractableObject
{
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    public GateScript targetGate;

    bool triggered = false;

    public void Interact(GameObject origin)
    {
        if (targetGate == null) return;

        if(triggered)
        {
            targetGate.CloseGate();
            triggered = false;
            GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }
        else
        {
            targetGate.OpenGate();
            triggered = true;
            GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
    }
}
