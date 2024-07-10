using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public TMP_Text pointsText;
    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString();
    }


    public void RestartButton() {
        SceneManager.LoadScene(1);
    }

    public void ExitButton() {
        SceneManager.LoadScene(0);
    }
}
