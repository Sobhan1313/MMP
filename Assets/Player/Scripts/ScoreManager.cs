using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; // UI-Text zur Anzeige der aktuellen Punkte
    public TextMeshProUGUI highScoreText; // UI-Text zur Anzeige des HighScores

    public static int currentScore;
    public static int highScore;

    void Awake()
    {
        // Singleton-Pattern, um sicherzustellen, dass es nur eine Instanz von ScoreManager gibt und keine Duplikate bei laden einer neuen Szene entstehen
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentScore = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreText();
        CheckHighScore();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "XP: " + currentScore.ToString();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "HighScore: " + highScore.ToString();
    }

    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }
}
