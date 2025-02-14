using UnityEngine;

public class lives : MonoBehaviour, IPickup
{
    public void Pickup(PlayerController player)
    {
        player.lives ++;

        Destroy(gameObject);
    }


}
