using UnityEngine;

public class Arrays : MonoBehaviour
{
    public GameObject[] prefabList;

     void Start()
    {
        Instantiate(prefabList[Random.Range(0, prefabList.Length)], transform.position, transform.rotation);
    }
}