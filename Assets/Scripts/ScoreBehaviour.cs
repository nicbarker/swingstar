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
    public AudioClip startTimerHigh;
    public AudioClip startTimerLow;
    private bool gameOver;
    private int previousPosition;
    public int score;
    private float countdown = 4;
    private SpriteRenderer glowEffectSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        glowEffectSpriteRenderer = GameObject.Find("GlowEffect").GetComponent<SpriteRenderer>();
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
                if (centerText.GetComponent<Text>().text != floorTime.ToString())
                {
                    GetComponents<AudioSource>()[0].clip = startTimerLow;
                    GetComponents<AudioSource>()[0].Play();
                }
                centerText.GetComponent<Text>().text = floorTime.ToString();
            } else if (centerText.GetComponent<Text>().text != "GO!")
            {
                centerText.GetComponent<Text>().text = "GO!";
                GetComponents<AudioSource>()[1].clip = startTimerHigh;
                GetComponents<AudioSource>()[1].Play();
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
            glowEffectSpriteRenderer.color = new Color(1, 1, 1, Mathf.Min(5, multiplier) * 0.2f);
            if (player.transform.position.x > previousPosition)
            {
                score += (int)(player.transform.position.x - previousPosition) * multiplier;
                previousPosition = (int)player.transform.position.x;
                scoreText.GetComponent<Text>().text = score.ToString();
            }
        }
    }
}
