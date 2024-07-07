using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2Controller : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public string target;    //Ziel, welches Benutzer angibt
    private GameObject targetFound; // Zielpunkt, auf den sich der Alien zubewegt
    private Rigidbody2D rb2d;
    private Animator animator;
    public GameObject Explosion;
   
    private AlienSpawner alienSpawner;
    private bool isDestroyed = false;

    [SerializeField] float maxHealth;
    FloatingHealthBar2 healthbar;

    private float health; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthbar = GetComponentInChildren<FloatingHealthBar2>();
        health = maxHealth;
        healthbar.UpdateHealthBar(health,maxHealth);
        animator = GetComponent<Animator>();
        alienSpawner = FindObjectOfType<AlienSpawner>();
        if (target != null){
        targetFound = GameObject.Find(target); // Beispiel: Finde das GameObject mit dem Namen "Player"
        }
    }
     private void TakeDamage(float damageAmount){

        health -= damageAmount;
        healthbar.UpdateHealthBar(health,maxHealth);
         

    }

    void FixedUpdate()
    {
        if(isDestroyed) return;
        MoveTowardsTarget();
        RotateTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (targetFound != null)
        {
            // Berechne die Richtung zum Zielpunkt
            Vector2 direction = (targetFound.transform.position - transform.position).normalized;
            rb2d.velocity = direction * speed;

        
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
        TakeDamage(1);
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
            Destroy(explosionInstance, 1.0f);
        }
    }

    
    }
}
