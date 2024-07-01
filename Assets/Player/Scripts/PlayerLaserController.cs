using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserController : MonoBehaviour
{
    [SerializeField]
    public float speed = 10.0f; // Geschwindigkeit des Lasers
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2d.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // Laser zerstören nach Kollision
        rb2d.velocity = Vector2.zero;
        Destroy(gameObject);
       
    }
}
