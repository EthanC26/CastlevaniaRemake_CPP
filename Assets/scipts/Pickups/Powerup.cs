using UnityEngine;
using UnityEngine.Audio;

public class Powerup : MonoBehaviour, IPickup
{
    public AudioClip pickupSound;
   
    AudioSource audioSource;
    public void Pickup()
    {
        GameManager.Instance.PlayerInstance.SpeedChange();
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, pickupSound.length);
    }

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
       
    }
   
    void Update ()
    {
       
    }
}
