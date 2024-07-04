using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien3Controller : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public string target;    //Ziel, welches Benutzer angibt
    [SerializeField]
    private float zigzagFrequency; // Wie oft sich die Richtung ändert
    [SerializeField]
    private float zigzagAmplitude; // Wie weit die Bewegung seitlich geht
    [SerializeField]
    private int health = 2;
    private GameObject targetFound; // Zielpunkt, auf den sich der Alien zubewegt
    private Rigidbody2D rb2d;
    private Animator animator;
    public GameObject Explosion;
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
        MoveTowardsTargetInZigZag();
        RotateTowardsTarget();
    }

    void MoveTowardsTargetInZigZag()
    {
        if (targetFound != null)
        {
            // Berechne die Richtung zum Zielpunkt
            Vector2 direction = (targetFound.transform.position - transform.position).normalized;

            // Orthogonale Richtung zur Hauptbewegungsrichtung berechnen
            Vector2 orthogonalDirection = new Vector2(-direction.y, direction.x).normalized;

            // Zick-Zack-Bewegung entlang der orthogonalen Richtung hinzufügen
            Vector2 zigzagOffset = orthogonalDirection * Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

            // Kombinierte Bewegungsrichtung
            Vector2 combinedDirection = direction + zigzagOffset;

            rb2d.velocity = combinedDirection * speed;

        
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
