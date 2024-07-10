using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; // UI-Element zur Anzeige der aktuellen Punkte
    public TextMeshProUGUI highScoreText; // UI-Element zur Anzeige des HighScores

    private int currentScore;
    private int highScore;

    void Awake()
    {
        // Singleton-Pattern, um sicherzustellen, dass es nur eine Instanz von ScoreManager gibt und keine Duplikate bei laden einer neuen Szene entstehen
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  //Bei Szenenwechsel nicht löschen!
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
        scoreText.text = "EP: " + currentScore.ToString();
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
