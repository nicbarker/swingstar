using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreBehaviour : MonoBehaviour
{
    public GameObject centerText;
    public GameObject player;
    public GameObject scoreText;
    public GameObject multiplierText;
    public ScoreManager scoreManager;
    private bool gameOver;
    private int previousPosition;
    public int score;
    private float countdown = 4;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            int floorTime = (int)countdown;
            if (floorTime > 0)
            {
                centerText.GetComponent<Text>().text = floorTime.ToString();
            } else
            {
                centerText.GetComponent<Text>().text = "GO!";
            }
        } else
        {
            if (multiplierText != null)
            {
                Destroy(centerText);
            }
            if (player == null)
            {
                if (!gameOver)
                {
                    gameOver = true;
                    scoreManager.TryAddingNewHighScore(score);
                }
                return;
            }

            int multiplier = Mathf.Max((int)(player.GetComponent<Rigidbody2D>().velocity.magnitude / 5), 1);
            multiplierText.GetComponent<Text>().text = multiplier + "X";
            if (player.transform.position.x > previousPosition)
            {
                score += (int)(player.transform.position.x - previousPosition) * multiplier;
                previousPosition = (int)player.transform.position.x;
                scoreText.GetComponent<Text>().text = score.ToString();
            }
        }
    }
}
