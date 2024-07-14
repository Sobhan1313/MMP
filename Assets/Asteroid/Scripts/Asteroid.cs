using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    private Rigidbody2D rb;
    private PolygonCollider2D pc;
    private SpriteRenderer sr;
    public Sprite[] sprites;
    public Spawner spawner;

    public GameObject Explosion;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
    }

    public void kick(float theMass, Vector2 direction){
        sr.sprite = this.sprites[Random.Range(0,this.sprites.Length)];

        List<Vector2> path = new List<Vector2>();
        sr.sprite.GetPhysicsShape(0,path);
        pc.SetPath(0,path.ToArray());

        rb.mass = theMass;
        float width =Random.Range(0.75f,1.33f);
        float height = 1/width;
        transform.localScale = new Vector2(width,height) * theMass;

        rb.velocity = direction.normalized * speed;
        rb.AddTorque(Random.Range(-4f,4f));
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Laser")|| collision.gameObject.CompareTag("Player")){
            
            GameObject explosionInstance = Instantiate(
                Explosion,
                transform.position,
                transform.rotation
            );
            Destroy(explosionInstance, 1.0f);
            Destroy(this.gameObject);
            spawner.Spawn();
            Debug.Log("from Asteroid ");
        }
        
    }
    


}
