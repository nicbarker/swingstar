using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum MenuPanel {
    MainMenu,
    HighScores,
    Credits
}
public class GameOverScreenBehaviour : MonoBehaviour
{
    public GameObject player;
    public string nextScene;
    private MenuPanel? visiblePanel;
    private float countdown = 1;
    private Canvas mainMenuCanvas;
    void showPanel(MenuPanel panel) {
        visiblePanel = panel;
        mainMenuCanvas.GetComponent<Canvas>().enabled = panel == MenuPanel.MainMenu;
        GameObject.Find("HighScoresCanvas").GetComponent<Canvas>().enabled = panel == MenuPanel.HighScores;
        var creditsCanvas = GameObject.Find("CreditsCanvas");
        if (creditsCanvas) {
            creditsCanvas.GetComponent<Canvas>().enabled = panel == MenuPanel.Credits;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var mainMenu = GameObject.Find("MainMenuCanvas");
        if (mainMenu != null) {
            mainMenuCanvas = mainMenu.GetComponent<Canvas>();
        }

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            switch (button.name) {
                case "NewGameButton": button.onClick.AddListener(() => GameObject.Find("FadeOutPanel").GetComponent<FadeOutBehaviour>().StartFade(() => SceneManager.LoadScene(nextScene))); break;
                case "HighScoresButton": button.onClick.AddListener(() => { showPanel(MenuPanel.HighScores); }); break;
                case "CreditsButton": button.onClick.AddListener(() => { showPanel(MenuPanel.Credits); }); break;
                case "BackButton": button.onClick.AddListener(() => { showPanel(MenuPanel.MainMenu); }); break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else if (visiblePanel == null)
            {
                showPanel(MenuPanel.MainMenu);
            }
        }
    }
}
