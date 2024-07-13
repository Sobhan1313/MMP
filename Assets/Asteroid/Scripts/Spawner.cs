using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    public Asteroid asteroid;

    float spawnDistance = 200f;


    void Start()
    {

        for (int i = 0; i < 20; i++)
        {
            Spawn();
        }



    }


    public void Spawn()
    {


        Vector2 spawnPoint = Random.insideUnitCircle.normalized * Random.Range(14, spawnDistance);

        float angle = Random.Range(-15f, 15f);

        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        Asteroid theAsteroid = Instantiate(asteroid, spawnPoint, rotation);

        Vector2 direction = rotation * -spawnPoint;
        float mass = Random.Range(0.8f, 1.4f);
        theAsteroid.kick(mass, direction);

    }
}
