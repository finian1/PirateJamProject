using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VisionScript : MonoBehaviour
{
    public bool canSeeTarget = false;

    public GameObject playerObject;
    public GameObject closestTarget;
    private float closestTargetDistance = 999999.0f;
    public List<GameObject> targets = new List<GameObject>();

    public GameObject[] objectsToIgnore;


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

        if(canSeeTarget && closestTarget)
        {
            currentForwardVector = closestTarget.transform.position - transform.position;
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
    }

    public void UpdateVision(Vector2 forwardVector)
    {
        closestTargetDistance = 999999.0f;
        canSeeTarget = false;
        targets.Clear();

        float currentAngle = -fieldOfView / 2.0f;
        Vector2 currentDirectionalVector;
        //Hit everything but enemies
        int layerMask = ~LayerMask.GetMask("Enemy");

        //temporarily disable colliders on objects to ignore
        foreach(GameObject ignoreTarget in objectsToIgnore )
        {
            Collider2D collider = ignoreTarget.GetComponent<Collider2D>();
            if( collider != null )
            {
                collider.enabled = false;
            }
        }

        for(int i = 0; i < numOfRays; i++)
        {
            float dirVectorX = Mathf.Cos(currentAngle) * forwardVector.x - Mathf.Sin(currentAngle) * forwardVector.y;
            float dirVectorY = Mathf.Sin(currentAngle) * forwardVector.x + Mathf.Cos(currentAngle) * forwardVector.y;
            currentDirectionalVector = new Vector2(dirVectorX, dirVectorY);

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, currentDirectionalVector, visionDistance);
            if(hit)
            {
                if (!hit.collider.gameObject.CompareTag(enemyStateManager.gameObject.tag) && 
                    (
                    hit.collider.gameObject.CompareTag("Player") ||
                    hit.collider.gameObject.CompareTag("Shadow") ||
                    hit.collider.gameObject.CompareTag("LightAlchemist")
                    ))
                {
                    canSeeTarget = true;
                    //playerObject = hit.collider.gameObject;
                    float dist = (hit.collider.gameObject.transform.position - transform.position).magnitude;
                    if(dist <= closestTargetDistance)
                    {
                        closestTargetDistance = dist;
                        closestTarget = hit.collider.gameObject;
                    }
                    targets.Add(hit.collider.gameObject);
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

        foreach (GameObject ignoreTarget in objectsToIgnore)
        {
            Collider2D collider = ignoreTarget.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

}
