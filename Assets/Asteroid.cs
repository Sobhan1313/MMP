using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
public class AsteroidController : MonoBehaviour { 
[SerializeField]     
private float speed = 2f; 
private Rigidbody2D rb2D; 
void Start() { 
        rb2D = GetComponent<Rigidbody2D>(); 
        rb2D.velocity = new Vector2(0, -speed);
    } 
} 
