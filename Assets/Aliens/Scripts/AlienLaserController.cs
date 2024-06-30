using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLaserController : MonoBehaviour
{
    [SerializeField]
    public float speed; // Geschwindigkeit des Lasers


    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Laser zerstören nach Kollision
        Destroy(gameObject);
    }
}
