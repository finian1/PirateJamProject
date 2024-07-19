using UnityEngine;

public class BreakableObject : MonoBehaviour, IInteractionEvents
{

    public float HP = 10.0f;
    [Header("Damage Settings")]
    public float minimumDamageThreshold = 5.0f;
    public float impactDamageSensitivity = 1.0f;
    //If object is fragile, it will instantly break if damage received is above the minimum threshold.
    public bool isFragile = false;

    public float launchForceAmplifier = 10.0f;

    private float prevVelocityMagnitude;

    void FixedUpdate()
    {
        prevVelocityMagnitude = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage(prevVelocityMagnitude * impactDamageSensitivity, null);
    }

    public void Damage(float amount, GameObject origin)
    {
        if (origin != null)
        {
            Vector2 forceDirection = transform.position - origin.transform.position;
            forceDirection.Normalize();
            gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * amount * launchForceAmplifier);
        }

        if (amount < minimumDamageThreshold)
        {
            return;
        }

        HP -= amount;
        if(isFragile || HP <= 0.0f)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        //Start destruction animation here.
        //Animation should call "Destroy(this object)" when done.
        Destroy(gameObject);
    }
}
