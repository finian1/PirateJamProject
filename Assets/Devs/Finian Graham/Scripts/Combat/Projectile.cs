using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    public float damageToDeal;
    public float projectileSpeed;

    CircleCollider2D hitCollider;
    // Start is called before the first frame update
    void Start()
    {
        hitCollider = GetComponent<CircleCollider2D>();
        hitCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            return;
        }
        ExecuteEvents.Execute<IDamageableObject>(collision.gameObject, null, (message, data) => message.Damage(damageToDeal, gameObject));
        Destroy(gameObject);
    }
}
