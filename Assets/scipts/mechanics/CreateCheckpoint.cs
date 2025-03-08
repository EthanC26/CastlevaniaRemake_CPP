using UnityEngine;

public class CreateCheckpoint : MonoBehaviour
{
    bool checkpointTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !checkpointTriggered)
            GameManager.Instance.UpdateCheckpoint(transform);
        Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
    }
}
