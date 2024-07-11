using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlienSpawner : MonoBehaviour
{
    public GameObject[] Aliens; // Prefab der Aliens, welche gespawnt werden
    [SerializeField]
    private int numberOfAliens; // Anfangsanzahl der Aliens
    [SerializeField]
    private int AlienAdditionRate = 1; // Anzahl der addierten Aliens pro Runde
    [SerializeField] 
    public TextMeshProUGUI alienCountText; // Text-Element zur Anzeige der Alien-Anzahl
    private int alienCount;
    private int waveNumber = 1;
    private List<GameObject> selectedAliens = new List<GameObject>(); // Liste für die ausgewählten Aliens
    [SerializeField]
    private float secondsBetweenWaves; // Sekunden zwischen den Wellen

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Infinite loop to continuously spawn waves
        {
            SpawnAliens();
            yield return new WaitForSeconds(secondsBetweenWaves); // Wait for specified seconds before spawning the next wave
            numberOfAliens += AlienAdditionRate; // add x aliens for the next spawn wave
            waveNumber++;
        }
    }

    void UpdateSelectedAliens()
    {
        selectedAliens.Clear(); // Leert die Liste

        switch (waveNumber)
        {
            case 1:
                selectedAliens.Add(Aliens[0]);
                selectedAliens.Add(Aliens[3]);
                break;
            case 2:
                selectedAliens.Add(Aliens[0]);
                selectedAliens.Add(Aliens[1]);
                break;
            case 3:
                selectedAliens.Add(Aliens[0]);
                selectedAliens.Add(Aliens[2]);
                break;
            case 4:
                selectedAliens.Add(Aliens[0]);
                selectedAliens.Add(Aliens[3]);
                break;
            default:
                selectedAliens.Add(Aliens[0]);
                selectedAliens.Add(Aliens[1]);
                selectedAliens.Add(Aliens[2]);
                selectedAliens.Add(Aliens[3]); 
                break;
        }
    }

    void SpawnAliens()
    {
        UpdateSelectedAliens(); // Aktualisiert die Liste der ausgewählten Aliens

        for (int i = 0; i < numberOfAliens; i++)
            {
                Vector2 spawnPosition = GetRandomPositionAtEdge();  // zufällige Spawn-Position am Rand der Spielfläche
                Quaternion spawnRotation = GetRotationTowardsCenter(spawnPosition);  // Ausrichtung der Aliens Richtung Zentrum
              
                GameObject selectedAlien = selectedAliens[Random.Range(0, selectedAliens.Count)];                // Wähle zufällig ein Alien-Prefab aus der Liste der für die jeweilige Runde ausgewählten Aliens aus
                                 
                Instantiate(selectedAlien, spawnPosition, spawnRotation);       // Instanziere das ausgewählte Alien-Prefab
                alienCount++;
            }
        UpdateAlienCountText();
               
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
                y = spawnHeight / 2;
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

    public void AlienDestroyed()
    {
        alienCount--;
        UpdateAlienCountText();
    }

    void UpdateAlienCountText()
    {
        alienCountText.text = "Aliens: " + alienCount;
    }
}
