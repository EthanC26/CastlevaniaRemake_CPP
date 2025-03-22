using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public void Pickup()
    {
        GameManager.Instance.PlayerInstance.SpeedChange();
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, pickupSound.length);
    }

    void Start() => audioSource = GetComponent<AudioSource>();

   
}


