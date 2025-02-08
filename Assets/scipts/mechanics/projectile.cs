using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class projectil : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("powerup"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Colliders"))
        {
            Destroy(gameObject);
        }
    }
}