﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenBehaviour : MonoBehaviour
{
    public GameObject player;
    public string nextScene;
    private bool gameOverScreenVisible;
    private bool highScoresScreenVisible;
    private float countdown = 1;
    private Canvas mainMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        var mainMenu = GameObject.Find("MainMenuCanvas");
        if (mainMenu != null) {
            mainMenuCanvas = mainMenu.GetComponent<Canvas>();
        }

        gameOverScreenVisible = false;
        highScoresScreenVisible = false;
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.name == "NewGameButton")
            {
                button.onClick.AddListener(() => GameObject.Find("FadeOutPanel").GetComponent<FadeOutBehaviour>().StartFade(() => SceneManager.LoadScene(nextScene)));
            } else if (button.name == "HighScoresButton")
            {
                button.onClick.AddListener(() =>
                {
                    highScoresScreenVisible = true;
                    gameOverScreenVisible = false;
                    mainMenuCanvas.GetComponent<Canvas>().enabled = false;
                    GameObject.Find("HighScoresCanvas").GetComponent<Canvas>().enabled = true;
                });
            }
            else if (button.name == "HighScoresBackButton")
            {
                button.onClick.AddListener(() =>
                {
                    highScoresScreenVisible = false;
                    gameOverScreenVisible = true;
                    mainMenuCanvas.GetComponent<Canvas>().enabled = true;
                    GameObject.Find("HighScoresCanvas").GetComponent<Canvas>().enabled = false;
                });
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
            else if (!highScoresScreenVisible && !gameOverScreenVisible)
            {
                // Game over
                gameOverScreenVisible = true;
                mainMenuCanvas.enabled = true;
            }
        }
    }
}
