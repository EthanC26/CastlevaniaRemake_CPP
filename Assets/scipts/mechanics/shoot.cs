using UnityEngine;

public class shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource audioSource;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

    [SerializeField] private projectil projectilePrefab;
    [SerializeField] private AudioClip axeSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
       audioSource = GetComponent<AudioSource>();

        if (initShotVelocity == Vector2.zero)
        {
            Debug.Log("init Shot Velocity has been changed to a default value");
            initShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log($"Please set default values on {gameObject.name}");
           
    }

    public void Fire()
    {
        projectil curProjectile;
        if (!sr.flipX)
        {
            
            curProjectile = Instantiate(projectilePrefab,spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
        }
        else 
        {
            
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(initShotVelocity);

        }

        audioSource.PlayOneShot(axeSound);
    }
   
}
