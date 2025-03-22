using UnityEngine;

public class lives : MonoBehaviour, IPickup
{
    AudioSource audioSource;
    public AudioClip pickupSound;
    public int livesToAdd;
    public void Pickup()
    {
       GameManager.Instance.lives += livesToAdd;
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, pickupSound.length);
    }
    void Start() => audioSource = GetComponent<AudioSource>();

}
