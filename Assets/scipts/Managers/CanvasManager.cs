using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;


[Obsolete]
public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startBtn;
    public Button settingsBtn;
    public Button backBtn;

    public Button quitBtn;
    public Button resumeBtn;
    public Button mainMenuBtn;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    

    [Header("Text")]
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;

    private void OnResume()//this is so the resume button will work for the pause menu
    {
        setMenus(null, pauseMenu);
        Time.timeScale = 1.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        if (startBtn) startBtn.onClick.AddListener(() => SceneManager.LoadScene("Level"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => setMenus(settingsMenu, mainMenu));
        if (backBtn) backBtn.onClick.AddListener(() => setMenus(mainMenu, settingsMenu));
        if(quitBtn) quitBtn.onClick.AddListener(QuitGame);
        if (resumeBtn) resumeBtn.onClick.AddListener(OnResume);
        if (mainMenuBtn) mainMenuBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScreen"));

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }
        if (livesText)
        {
           // GameManager.Instance.OnLifeValueChanged.AddListener(OnLifeValueChanged);
            OnLifeValueChanged(GameManager.Instance.lives);
        }
    }

    private void OnLifeValueChanged(int value) => livesText.text = $"Lives: {GameManager.Instance.lives}";
    
    

    private void OnSliderValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value * 100);
        if (volSliderText) volSliderText.text = $"{roundedValue}%";
    }

    private void OnDisable()
    {
        if (startBtn) startBtn.onClick.RemoveAllListeners();
        if (settingsBtn) settingsBtn.onClick.RemoveAllListeners();
        if (backBtn) backBtn.onClick.RemoveAllListeners();
        if (quitBtn) quitBtn.onClick.RemoveAllListeners();
        if (resumeBtn) resumeBtn.onClick.RemoveAllListeners();
        if (mainMenuBtn) mainMenuBtn.onClick.RemoveAllListeners();
        //if (livesText) GameManager.Instance.OnLifeValueChanged.RemoveAllListeners();
    }

    private void setMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate) menuToActivate.SetActive(true);
        if (menuToDeactivate) menuToDeactivate.SetActive(false);
    }

    private void QuitGame()
    {
     #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
     #else
        Application.Quit();
     #endif
    }
       
    

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            
            if(pauseMenu.activeSelf)
            {
                //do something - hint for lab
                Time.timeScale = 0.0f;
            }
          
            else
            {
                //do something else
                //only works with pressing P
                Time.timeScale = 1.0f;
            }
        }
    }
}
