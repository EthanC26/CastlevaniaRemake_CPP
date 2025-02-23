using Unity.VisualScripting;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    private IPickup[] ranDrop;
    private IPickup selectedPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ranDrop = new IPickup[]
        {
            new lives(),
            new score(),
            new Powerup()
        };
        
       IPickup selectedPower = ranDrop[Random.Range(0, ranDrop.Length)];

        Debug.Log("Selected PowerUp: " + selectedPower.GetType().Name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IPickup pickup = GetComponent<IPickup>();

        }
        Destroy(gameObject);
    }
}
