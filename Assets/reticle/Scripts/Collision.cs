using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public GameOverScreen gameOver;
    private AlienSpawner alienSpawner;
    public GameObject healthBar;
    public GameObject player;

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
            Destroy(player);
            Destroy(healthBar);
            gameOver.Setup(ScoreManager.highScore);
            Time.timeScale = 0f;
        }
    }
}
