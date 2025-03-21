using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))] 
public abstract class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    AudioSource audioSource;

    protected int health;
    [SerializeField] protected int maxHealth;

    public AudioClip DeathClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int DamageValue, DamageType damage = DamageType.Default)
    {
        health -= DamageValue;

        if (health <= 0)
        {
            audioSource.PlayOneShot(DeathClip);

            anim.SetTrigger("Death");

            if (transform.parent != null) Destroy(transform.parent.gameObject, 0.5f);
            else Destroy(gameObject, 0.5f);
        }
    }
}

public enum DamageType
{
    Default,
    jumpedOn,
}