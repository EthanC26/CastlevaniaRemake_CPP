using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class projectil : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;
    [SerializeField, Range(1, 20)] private int Damage = 20;
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
        if (gameObject.CompareTag("pProj"))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
        if(gameObject.CompareTag("eProj") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives--;
            Destroy(gameObject);
        }

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