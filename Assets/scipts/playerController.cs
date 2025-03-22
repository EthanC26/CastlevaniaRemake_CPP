using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]//properties of a class
[RequireComponent (typeof(GroundCheck), typeof(Jump), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gndck;

    private Coroutine speedChange = null;

    

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

    //Audio Clips
    private AudioSource audioSource;
    [SerializeField] private AudioClip whipSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//grabs rigidbody2D
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gndck = GetComponent<GroundCheck>();
        audioSource = GetComponent<AudioSource>();

      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;
            
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        
        //main attack
        if (Input.GetButtonDown("attack"))
            if (elapsedTime > attackTimer)
            {
                Attacking = true;

                audioSource.PlayOneShot(whipSound);

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
       //if (pickup != null) pickup.Pickup(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //detect pickup
        IPickup pickup = collision.GetComponent<IPickup>();
        if (pickup != null) pickup.Pickup();
    }
    
    public void SpeedChange()
    {
        if (speedChange != null)
        {
            StopCoroutine(speedChange);
            speed /= 2;
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