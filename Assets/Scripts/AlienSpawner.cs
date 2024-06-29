using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject Alien; // Prefab der Aliens, welche gespawnt werden
    [SerializeField]
    public int numberOfAliens; // Anzahl der Aliens, die gespawnt werden sollen

    void Start()
    {
        SpawnAliens();
    }

    void SpawnAliens()
    {
        for (int i = 0; i < numberOfAliens; i++)
        {
            Vector2 spawnPosition = GetRandomPositionAtEdge();  // zufällige Spawn-Position am Rand der Spielfläche
            Quaternion spawnRotation = GetRotationTowardsCenter(spawnPosition);  // Ausrichtung der Aliens Richtung Zentrum
            Instantiate(Alien, spawnPosition, spawnRotation);
        }
    }

    Vector2 GetRandomPositionAtEdge()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float scale = 50;
        float spawnHeight = cameraHeight * scale;
        float spawnWidth = spawnHeight * Camera.main.aspect;

        float x = 0f;
        float y = 0f;

        // Zufällig eine der vier Kanten auswählen
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Obere Kante
                x = Random.Range(-spawnWidth / 2, spawnWidth / 2);
                y = spawnHeight / 2 ;
                break;
            case 1: // Untere Kante
                x = Random.Range(-spawnWidth / 2, spawnWidth / 2);
                y = -spawnHeight / 2;
                break;
            case 2: // Linke Kante
                x = -spawnWidth / 2;
                y = Random.Range(-spawnHeight / 2, spawnHeight / 2);
                break;
            case 3: // Rechte Kante
                x = spawnWidth / 2;
                y = Random.Range(-spawnHeight / 2, spawnHeight / 2);
                break;
        }

        return new Vector2(x, y);
    }

    Quaternion GetRotationTowardsCenter(Vector2 spawnPosition)
    {
        Vector2 centerPosition = Vector2.zero; // Annahme: Zentrum ist Punkt (0, 0) 

        Vector2 direction = centerPosition - spawnPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return Quaternion.Euler(0, 0, angle);
    }
}