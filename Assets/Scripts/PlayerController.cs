using UnityEngine;
using UnityEngine.UI; 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool enableKeyboardControl;
    [SerializeField]
    private int maxHealth = 5; 
    [SerializeField]
    private Text healthText; 

    private int currentHealth;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; 
        UpdateHealthUI();
    }

    void FixedUpdate()
    {
        if (enableKeyboardControl)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
            rb2d.velocity = movement;
        }
        else
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb2d.MovePosition(mousePosition);
        }
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        Plane xyPlane = new Plane(Vector3.forward, Vector3.zero);

        float distance;
        if (xyPlane.Raycast(ray, out distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);
            
            Vector3 direction = mouseWorldPosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid")) 
        {
            TakeDamage(1); 
            Debug.Log("Ship go boom");
        }
        if (collision.gameObject.CompareTag("Alien")) 
        {
            TakeDamage(1); 
            Debug.Log("Ship go boom");
        }
        if (collision.gameObject.CompareTag("Laser")) 
        {
            TakeDamage(1); 
            Debug.Log("Ship go boom");
        }
    }

    void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage; 
            UpdateHealthUI();
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            Destroy(this.gameObject);
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
}
