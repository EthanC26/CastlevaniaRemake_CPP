using UnityEngine;
using UnityEngine.Audio;

public class score : MonoBehaviour, IPickup
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int scoreToAdd;
    public void Pickup()
    {
        GameManager.Instance.Score =+ scoreToAdd;
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, pickupSound.length);
    }

    void Start() => audioSource = GetComponent<AudioSource>();
    

}
