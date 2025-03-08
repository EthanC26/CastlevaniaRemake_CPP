using UnityEngine;
//this script is fine for a small scope - Pickups that remain fairly similar in their usecase - 
//but anything more than 10 pickups and varied mechanics fr the pickups will probably require a diffrent solution
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup,
        Score
    }

    public PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController PC =collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                case PickupType.Powerup:
                    PC.SpeedChange();
                    break;
                case PickupType.Score:
                    GameManager.Instance.Score++;
                    break;
                    
            }

            Destroy(gameObject);
        }
    }
}
