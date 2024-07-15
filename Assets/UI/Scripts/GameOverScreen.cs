using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text reasonText;

    public void Setup(int score, int highScore, string reason) {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "Highscore: " + highScore.ToString();
        reasonText.text = reason;
    }


    public void RestartButton() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ExitButton() {
        SceneManager.LoadScene(0);
    }
}
