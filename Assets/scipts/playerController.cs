using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]//properties of a class
[RequireComponent (typeof(GroundCheck), typeof(Jump))]
public class PlayerController : MonoBehaviour
{
    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gndck;

    private int maxLives = 10;
    private int _lives = 5;

    private Coroutine speedChange = null;

 
  
    public int lives
    {
        get => _lives;
        set
        {
            _lives = value;
            if (_lives > maxLives )  _lives = maxLives;

            Debug.Log($"player Controller lives has changed to {_lives}");
        }
    }

    private int _Score = 0;

    public int Score
    {
        get=> _Score;
        set
        {
            _Score = value;

            Debug.Log($"player Controller score has changed to {_Score}");
        }
            


    }

    public int GetLives() { return lives; }
    public void SetLives(int value ) 
    { 
        lives = value;
        if (lives >maxLives)
            lives = maxLives;
    }

    //movement variables
    [Range(1, 3)]
    public float speed = 1.0f;
    [Range(1,4)]
    public float jumpforce = 2.0f;

    public bool isGrounded = false;

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
        gndck = GetComponent<GroundCheck>();

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

       

        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        
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
            if (rb.linearVelocity.y <= 0) isGrounded = gndck.isGrounded();
           
        }
        else isGrounded = gndck.isGrounded();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //detect pickup
        IPickup pickup = collision.gameObject.GetComponent<IPickup>();
        if (pickup != null) pickup.Pickup(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //detect pickup
        IPickup pickup = collision.GetComponent<IPickup>();
        if (pickup != null) pickup.Pickup(this);
    }
    private void OnCollisionExit(Collision collision)
    {

    }

    
    public void SpeedChange()
    {
        if (speedChange != null)
        {
            StopCoroutine(speedChange);
           // speed /= 2;
        }    
          
        speedChange = StartCoroutine(SpeedChangeCoroutine());
    }
    IEnumerator SpeedChangeCoroutine()
    {
        //do something immediately
        speed *= 2;
        Debug.Log($"player Controller speed has changed to {speed}");
        yield return new WaitForSeconds(5.0f);

        //do something after 5 seconds
        speed /= 2;
    }
}