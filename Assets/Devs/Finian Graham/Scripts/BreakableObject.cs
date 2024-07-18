using UnityEngine;

public class BreakableObject : MonoBehaviour, IInteractionEvents
{

    public float HP = 10.0f;
    [Header("Damage Settings")]
    public float minimumDamageThreshold = 5.0f;
    public float impactDamageSensitivity = 1.0f;
    //If object is fragile, it will instantly break if damage received is above the minimum threshold.
    public bool isFragile = false;

    private float prevVelocityMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        //ExecuteEvents.Execute<IInteractionEvents>(this.gameObject, null, (x, y) => x.Damage(10.0f));
    }

    void FixedUpdate()
    {
        prevVelocityMagnitude = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(prevVelocityMagnitude);
        Damage(prevVelocityMagnitude * impactDamageSensitivity);
    }

    public void Damage(float amount)
    {
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
        //Animation should call "Destroy(this)" when done.
        Destroy(gameObject);
    }
}
