using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private AlienSpawner alienSpawner;

    void Start()
    {
        alienSpawner = FindObjectOfType<AlienSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alien"))
        {
            Destroy(other.gameObject);
            alienSpawner.AlienDestroyed();
        }
    }
}
