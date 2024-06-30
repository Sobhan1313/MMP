using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien4Controller : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public string target;    //Ziel, welches Benutzer angibt
    [SerializeField]
    public float fireRate = 1.0f; // Feuerfrequenz in Sekunden
    public GameObject AlienLaser;
    private GameObject targetFound; // Zielpunkt, auf den sich der Alien zubewegt
    private Rigidbody2D rb2d;
    private Animator animator;
    [SerializeField]
    private int health;
    private float nextFireTime;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (target != null)
    {
        targetFound = GameObject.Find(target); // Beispiel: Finde das GameObject mit dem Namen "Player"
    }
    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
        RotateTowardsTarget();
        // Laser abfeuern
        if (Time.time >= nextFireTime)
        {
            FireLaser();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireLaser()
    {
        
        Instantiate(AlienLaser, transform.position, transform.rotation);

    }

    void MoveTowardsTarget()
    {
        if (targetFound != null)
        {
            // Berechne die Richtung zum Zielpunkt
            Vector2 targetDirection = (targetFound.transform.position - transform.position).normalized;
            rb2d.velocity = targetDirection * speed;

        
        }
        else
        {
            rb2d.velocity = Vector2.zero;
          
        }
    }

    void RotateTowardsTarget()
    {
        if (targetFound != null)
        {
            Vector2 direction = (targetFound.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
    {
        // Do nothing
        return;
    } else {
        health--;
        Debug.Log("Alien collided with " + collision.gameObject.name);
        if (health <= 0) {
             rb2d.velocity = Vector2.zero;
             Destroy(gameObject);    //Alien wird bei Kollision zerstört
        }
    }

    
    }
}
