using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private Rigidbody2D rb2D;
    private Collision2D c2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(0, -speed);
    }

    void OnCollisionEnter2D(Collision2D c2D)
    {
         if (c2D.gameObject.CompareTag("Reticle") &&  c2D.gameObject.CompareTag("Asteroid"))
            return;
        Debug.Log("Asteroid go boom");
        Destroy(this.gameObject);
    }
}
