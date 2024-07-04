using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien4Controller : MonoBehaviour
{
    [SerializeField]
    public float speed = 4.0f;
    [SerializeField]
    public string target;    //Ziel, welches Benutzer angibt
    [SerializeField]
    public float fireRate = 1.0f; // Feuerfrequenz in Sekunden
    public GameObject AlienLaser;
    private GameObject targetFound; // Zielpunkt, auf den sich der Alien zubewegt
    [SerializeField]
    public float minFireDistance = 5.0f; // Mindestabstand zum Zielobjekt
    private Rigidbody2D rb2d;
    private Animator animator;
    public GameObject Explosion;
    [SerializeField]
    private int health = 3;
    private float nextFireTime;
    private AlienSpawner alienSpawner;
    private bool isDestroyed = false;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        alienSpawner = FindObjectOfType<AlienSpawner>();
        if (target != null)
    {
        targetFound = GameObject.Find(target); // Beispiel: Finde das GameObject mit dem Namen "Player"
    }
    }

    void FixedUpdate()
    {
        if(isDestroyed) return;
        MoveTowardsTarget();
        RotateTowardsTarget();

        // Berechne die Entfernung zum Zielobjekt
        if(targetFound != null){
            float distanceToTarget = Vector2.Distance(transform.position, targetFound.transform.position);

            // Laser abfeuern, wenn der Mindestabstand erreicht ist
            if (Time.time >= nextFireTime && distanceToTarget <= minFireDistance)
            {
                FireLaser();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FireLaser()
    {

        // Verschiebungsdistanz
        float offsetDistance = 1.0f;

        Instantiate(AlienLaser, (transform.position + (transform.right * offsetDistance)), transform.rotation);

    }

    void MoveTowardsTarget()
    {
        if (targetFound != null)
        {
            // Berechnet die Richtung zum Zielpunkt
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
            if (alienSpawner != null && !collision.gameObject.CompareTag("Reticle"))
            {
                alienSpawner.AlienDestroyed();
            }
            GameObject explosionInstance = Instantiate(Explosion, transform.position, transform.rotation);
            isDestroyed = true;
            Destroy(gameObject);    //Alien wird bei Kollision zerstört
            Destroy(explosionInstance);
        }
    }

    
    }
}
