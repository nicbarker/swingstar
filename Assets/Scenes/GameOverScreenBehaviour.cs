using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOverScreenBehaviour : MonoBehaviour
{
    public GameObject player;
    public ScoreBehaviour score;
    private bool gameOverScreenVisible;
    private bool highScoresScreenVisible;
    private float countdown = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreenVisible = false;
        highScoresScreenVisible = false;
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.name == "RetryButton")
            {
                button.onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
            } else if (button.name == "HighScoresButton")
            {
                button.onClick.AddListener(() =>
                {
                    highScoresScreenVisible = true;
                    gameOverScreenVisible = false;
                    GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = false;
                    GameObject.Find("HighScoresCanvas").GetComponent<Canvas>().enabled = true;
                });
            }
            else if (button.name == "HighScoresBackButton")
            {
                button.onClick.AddListener(() =>
                {
                    highScoresScreenVisible = false;
                    gameOverScreenVisible = true;
                    GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true;
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
                GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
