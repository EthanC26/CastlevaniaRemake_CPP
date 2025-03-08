using UnityEngine;

public class score : MonoBehaviour, IPickup
{
    public int scoreToAdd;
    public void Pickup()
    {

        GameManager.Instance.Score =+ scoreToAdd;
        Destroy(gameObject);
    }


}
