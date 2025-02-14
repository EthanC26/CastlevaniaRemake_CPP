using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    public void Pickup(PlayerController player)
    {
        player.SpeedChange();

        Destroy(gameObject);
    }

   
}
