using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class TurretEnemy : Enemy
{
    [SerializeField] private float projectileFireRate = 2.0f;
    [SerializeField] private float detcRange = 2.0f;
    private float timeSinceLastFire = 0f;
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        base.Start();

        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (projectileFireRate <= 0)
            projectileFireRate = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("Idle")) CheckFire();

        TurretFlip();
        
    }

     void CheckFire()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= detcRange && Time.time > timeSinceLastFire + projectileFireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
    }

    private void TurretFlip()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x < transform.position.x)
            {
                sr.flipX = false;
            }

            else if(playerTransform.position.x > transform.position.x)
            {
                sr.flipX = true;
            }
        }
    }
}
