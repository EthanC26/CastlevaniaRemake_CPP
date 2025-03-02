using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]

public class WalkerEnemy : Enemy
{
    Rigidbody2D rb;

    [SerializeField] private float xVel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if(xVel <= 0) xVel = 1;
    }

    public override void TakeDamage(int DamageValue, DamageType damage = DamageType.Default)
    {
        base.TakeDamage(DamageValue, damage);
        Debug.Log("This is ectra code from walkerenemy.cs");
        rb.linearVelocityX = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("zombie walk"))
            rb.linearVelocity = (sr.flipX) ? new Vector2(xVel, rb.linearVelocityY) : new Vector2(-xVel,
                rb.linearVelocityY);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
            sr.flipX = !sr.flipX;
    }
}
