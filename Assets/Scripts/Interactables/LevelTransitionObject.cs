using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionObject : MonoBehaviour, IInteractableObject
{
    public int levelIndex;

    public LevelManagerotherscenes levelManager;

    public void Interact(GameObject origin)
    {
        //FindFirstObjectByType<LevelManager>().LoadScene(levelIndex);
        levelManager.LoadScene(levelIndex);
    }

    
}
