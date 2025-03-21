using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public event Action<PlayerController> OnPlayerSpawned;
    public event Action <int> OnLifeValueChanged;

    #region GAME PROPERTIES
    [SerializeField]private int maxLives = 10;
    private int _lives = 5;

    public int lives
    {
        get => _lives;
        set
        {
            //Debug.Log("current{ _lives}");
             

            if (value < 0)
            {
                GameOver();
                return;
            }
            if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"{gameObject.name} lives has changed to {_lives}");
        }
    }
    //public int GetLives() { return lives; }
    //public void SetLives(int value)
    //{
    //    lives = value;
    //    if (lives > maxLives)
    //        lives = maxLives;
    //}

    private int _Score = 0;

    public int Score
    {
        get => _Score;
        set
        {
            _Score = value;

            Debug.Log($"{gameObject.name} score has changed to {_Score}");
        }



    }
    #endregion
    #region PLAYER CONTROLLER INFO
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion


    private MenuController CurrentMenuController;
    private Transform currentCheckpoint;

    public void SetMenuController(MenuController newMenuController) => CurrentMenuController = newMenuController;
    void Awake()
    {
        Debug.Log($"current lives: { _lives}");

        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        if(maxLives <= 0) maxLives = 5;
        _lives = maxLives;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            string sceneName = (SceneManager.GetActiveScene().name.Contains("Level")) ? "TitleScreen" : "Level";
            SceneManager.LoadScene(sceneName);
            
            string sceneName_ = (SceneManager.GetActiveScene().name.Contains("GameOver")) ? "TitleScreen" : "GameOver";
            SceneManager.LoadScene(sceneName_);

            string _sceneName = (SceneManager.GetActiveScene().name.Contains("TitleScreen")) ? "Level" : "TitleScreen";
            SceneManager.LoadScene(_sceneName);

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Score++;

        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (CurrentMenuController.CurrentState.state == MenuStates.InGame)
                    CurrentMenuController.SetActiveState(MenuStates.Pause);
                else
                    CurrentMenuController.JumpBack();
            }
        }
    }
    
    

    void GameOver()
    {
        if (lives <= 0)
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("Level")) ? "GameOver" : "Level";
            SceneManager.LoadScene(sceneName);

            Debug.Log("Game Over gose here :(");
        }

        lives = 3;
        
    }

    void Respawn()
    {
        //TODO: ANIMATION BEFORE POSTION CHANGE
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void InstantiatePlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Transform updatedChechpoint)
    {
        currentCheckpoint = updatedChechpoint;
        Debug.Log("checkpoint updated"); 
    }
}
