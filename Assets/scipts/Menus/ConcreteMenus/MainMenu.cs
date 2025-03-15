using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public Button startBtn;
    public Button settingsBtn;
    public Button creditsBtn;
    public override void Init(MenuController contex)
    {
        base.Init(contex);
        state = MenuStates.MainMenu;

        if (startBtn) startBtn.onClick.AddListener(() => SceneManager.LoadScene("Level"));
        if (settingsBtn) settingsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.Settings));
        if (creditsBtn) creditsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.Credits));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
