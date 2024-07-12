using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public GameOverScreen gameOver;
    private AlienSpawner alienSpawner;

    void Start()
    {
        alienSpawner = FindObjectOfType<AlienSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alien") && alienSpawner != null)
        {
            Destroy(other.gameObject);
            alienSpawner.AlienDestroyed();
            gameOver.Setup(ScoreManager.highScore);
            Time.timeScale = 0f;
        }
    }
}
