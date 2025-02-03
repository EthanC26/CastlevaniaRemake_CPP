using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Range(1, 3)]
    public float speed = 1.0f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

       
    }
}