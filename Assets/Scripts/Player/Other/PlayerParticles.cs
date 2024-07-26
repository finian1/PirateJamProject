using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{

    public GameObject doubleJumpParticles;
    public GameObject playerFeet;
    public Quaternion rotation;

    public void DoubleJumpParticle()
    {
        Instantiate(doubleJumpParticles, playerFeet.transform.position, rotation);
    }
}
