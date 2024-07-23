using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private Vector3 dashOffset;

    private void Update()
    {
        transform.position = mainPlayer.transform.position + dashOffset;
    }

}
