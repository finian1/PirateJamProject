using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject mainPlayer;
    public float followSpeed;
    public Vector2 moveDirection;
    public float forceMultiplier;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = mainPlayer.transform.position - transform.position;
        moveDirection = moveDirection.normalized * forceMultiplier;

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("working.");
        //    rb.AddForce(-moveDirection * Time.deltaTime, ForceMode2D.Impulse);
        //}
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * followSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LightAttack"))
        {
            Debug.Log("Force applied.");
            rb.AddForce(-moveDirection * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

}
