using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractableObject
{
    public GateScript targetGate;

    bool triggered = false;

    public void Interact(GameObject origin)
    {
        if (targetGate == null) return;

        if(triggered)
        {
            targetGate.CloseGate();
            triggered = false;
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            targetGate.OpenGate();
            triggered = true;
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }
}
