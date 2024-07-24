using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VisionScript : MonoBehaviour
{
    public bool canSeePlayer = false;

    public GameObject playerObject;
    public Light2D visionLight;
    public EnemyStateManager enemyStateManager;

    public int numOfRays = 50;
    public float fieldOfView = 90.0f;
    public float visionDistance = 20.0f;

    private float angleStep;

    private void Start()
    {
        if (visionLight != null)
        {
            visionLight.pointLightInnerAngle = fieldOfView;
            visionLight.pointLightOuterAngle = fieldOfView + 5.0f;
            visionLight.pointLightInnerRadius = visionDistance / 2.0f;
            visionLight.pointLightOuterRadius = visionDistance;
        }

        fieldOfView *= Mathf.Deg2Rad;
        angleStep = fieldOfView / numOfRays;
    }

    private void FixedUpdate()
    {
        Vector2 currentForwardVector;

        if(canSeePlayer)
        {
            currentForwardVector = playerObject.transform.position - transform.position;
            currentForwardVector.Normalize();
        }
        else
        {
            if (enemyStateManager.movingRight)
            {
                currentForwardVector = transform.right;
            }
            else
            {
                currentForwardVector = -transform.right;
            }
        }

        UpdateVision(currentForwardVector);
        if(!canSeePlayer)
        {
            playerObject = null;
        }
    }

    public void UpdateVision(Vector2 forwardVector)
    {
        canSeePlayer = false;
        float currentAngle = -fieldOfView / 2.0f;
        Vector2 currentDirectionalVector;
        //Hit everything but enemies
        int layerMask = ~LayerMask.GetMask("Enemy");

        for(int i = 0; i < numOfRays; i++)
        {
            float dirVectorX = Mathf.Cos(currentAngle) * forwardVector.x - Mathf.Sin(currentAngle) * forwardVector.y;
            float dirVectorY = Mathf.Sin(currentAngle) * forwardVector.x + Mathf.Cos(currentAngle) * forwardVector.y;
            currentDirectionalVector = new Vector2(dirVectorX, dirVectorY);

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, currentDirectionalVector, visionDistance, layerMask);
            if(hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    canSeePlayer = true;
                    playerObject = hit.collider.gameObject;
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point);
                }
            }
            else
            {
                Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + currentDirectionalVector * visionDistance);
            }


            currentAngle += angleStep;
        }
    }

}
