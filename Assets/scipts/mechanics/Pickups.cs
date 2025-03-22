using UnityEngine;
//this script is fine for a small scope - Pickups that remain fairly similar in their usecase - 
//but anything more than 10 pickups and varied mechanics fr the pickups will probably require a diffrent solution
public class Pickups : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public enum PickupType
    {
        Life,
        Powerup,
        Score
    }

    public PickupType type;

    void Start() => audioSource = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController PC =collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    audioSource.PlayOneShot(pickupSound);
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case PickupType.Powerup:
                    PC.SpeedChange();
                    audioSource.PlayOneShot(pickupSound);
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case PickupType.Score:
                    GameManager.Instance.Score++;
                    audioSource.PlayOneShot(pickupSound);
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    break;
                    
            }

            Destroy(gameObject, pickupSound.length);
            
        }
    }
}
