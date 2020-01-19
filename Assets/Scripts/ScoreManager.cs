using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;

class HighScores {
    public List<int> scores;
}

public class ScoreManager : MonoBehaviour
{
    private HighScores highScores;
    private string gameDataFileName = "data.json";

    void Start()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            highScores = JsonUtility.FromJson<HighScores>(dataAsJson);
            UpdateHighScoreDisplay();
        }
        else
        {
            highScores = new HighScores();
            SaveGameData();
        }
    }

    private void SaveGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
        File.WriteAllText(filePath, JsonUtility.ToJson(highScores));
    }

    public void TryAddingNewHighScore(int score)
    {
        highScores.scores.Add(score);
        highScores.scores.Sort();
        highScores.scores.Reverse();
        try
        {
            highScores.scores = highScores.scores.GetRange(0, 5);
        } catch (ArgumentException e) {}
        SaveGameData();
        UpdateHighScoreDisplay();
    }

    private void UpdateHighScoreDisplay()
    {
        for (var i = 0; i < highScores.scores.Count; i++)
        {
            int newIndex = i + 1;
            GameObject.Find("HighScoreLabel" + newIndex).GetComponent<Text>().text = newIndex + ". " + highScores.scores[i];
        }
    }
}