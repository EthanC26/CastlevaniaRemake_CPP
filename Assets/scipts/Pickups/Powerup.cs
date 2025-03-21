using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    public void Pickup()
    {
        GameManager.Instance.PlayerInstance.SpeedChange();

        Destroy(gameObject);
    }


}
