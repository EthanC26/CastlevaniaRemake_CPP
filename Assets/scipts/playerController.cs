using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]//properties of a class
public class PlayerController : MonoBehaviour
{
    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    //movement variables
    [Range(1, 3)]
    public float speed = 1.0f;
    [Range(1,2)]
    public float jumpforce = 2.0f;

    //groundcheck variables
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;
    public bool isGrounded = false;

    private Transform groundCheck;

    //attack variables
    private bool Attacking = false;
    private bool SAttacking = false;
    //attack timers
    private float attackTimer = 1;
    private float elapsedTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//grabs rigidbody2D
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //groundcheck initaliaztion
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);//checking if player is grounded

        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        //main attack
        if (Input.GetButtonDown("attack"))
            if (elapsedTime > attackTimer)
            {
                Attacking = true;

                elapsedTime = 0;
            }
        //sub attack
        if (Input.GetButtonDown("subattack"))
            if (elapsedTime > attackTimer)
            {
                SAttacking = true;

                elapsedTime = 0;
            }

        //sprite flipping(right to left)
        if (hInput != 0) sr.flipX = (hInput > 0);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("Attacking", Attacking);
        anim.SetBool("subAttacking", SAttacking);

        //turns attacks false
        if (elapsedTime >= 0.5)
        {
            Attacking = false;

            SAttacking = false;
        }

        //timer
        elapsedTime += Time.deltaTime;
    }

    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
               isGroundLayer);
        }
        else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,isGroundLayer);
    }
}