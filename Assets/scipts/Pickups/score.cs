using UnityEngine;

public class score : MonoBehaviour, IPickup
{
    public void Pickup(PlayerController player)
    {
        player.Score += 10;

        Destroy(gameObject);
    }


}
