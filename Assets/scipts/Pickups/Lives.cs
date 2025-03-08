using UnityEngine;

public class lives : MonoBehaviour, IPickup
{
    public int livesToAdd;
    public void Pickup()
    {
       GameManager.Instance.lives += livesToAdd;

        Destroy(gameObject);
    }


}
