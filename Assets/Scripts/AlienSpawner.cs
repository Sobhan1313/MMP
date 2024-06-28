using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject Alien; // Prefab der Aliens, welche gespawnt werden
    [SerializeField]
    public int numberOfAliens; // Anzahl der Aliens, die gespawnt werden sollen
    [SerializeField]
    public float spawnAreaPadding; // Abstand vom Rand der Spielfläche, um Spawnpositionen zu berechnen z.B. 1.0f

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
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 70;
        float cameraWidth = cameraHeight * screenAspect;

        float x = 0f;
        float y = 0f;

        // Zufällig eine der vier Kanten auswählen
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Obere Kante
                x = Random.Range(-cameraWidth / 2 + spawnAreaPadding, cameraWidth / 2 - spawnAreaPadding);
                y = Camera.main.orthographicSize + spawnAreaPadding;
                break;
            case 1: // Untere Kante
                x = Random.Range(-cameraWidth / 2 + spawnAreaPadding, cameraWidth / 2 - spawnAreaPadding);
                y = -Camera.main.orthographicSize - spawnAreaPadding;
                break;
            case 2: // Linke Kante
                x = -cameraWidth / 2 - spawnAreaPadding;
                y = Random.Range(-cameraHeight / 2 + spawnAreaPadding, cameraHeight / 2 - spawnAreaPadding);
                break;
            case 3: // Rechte Kante
                x = cameraWidth / 2 + spawnAreaPadding;
                y = Random.Range(-cameraHeight / 2 + spawnAreaPadding, cameraHeight / 2 - spawnAreaPadding);
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