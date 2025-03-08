using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    //Expression Body Syntax for Functuons that can be one line of code
    void Start() => GameManager.Instance.InstantiatePlayer(startPos);
}