using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public GameObject PlayerLaser;
    private int currentHealth;
    private Rigidbody2D rb2d;
    private bool isDestroyed = false;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float damageInterval = 1f; // Damage interval in seconds

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        CalculateBounds();
        StartCoroutine(CheckBoundsAndDamage());
    }

    void FixedUpdate()
    {
        if (isDestroyed) return;
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
        if (isDestroyed) return;
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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        // Verschiebungsdistanz
        float offsetDistance = 2.0f;
        Instantiate(PlayerLaser, transform.position + (transform.right * offsetDistance), transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Reticle")) && (collision.gameObject.CompareTag("Asteroid"))) return;
        
        if (collision.gameObject.CompareTag("Alien") || collision.gameObject.CompareTag("Laser"))
        {
            TakeDamage(1);
            Debug.Log("Ship go boom");
        }
    }

    void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            UpdateHealthUI();
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void CalculateBounds()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float scale = 50; // Adjust this value as needed
        float spawnHeight = cameraHeight * scale;
        float spawnWidth = spawnHeight * Camera.main.aspect;

        minBounds = new Vector2(-spawnWidth / 2, -spawnHeight / 2);
        maxBounds = new Vector2(spawnWidth / 2, spawnHeight / 2);
    }

    private IEnumerator CheckBoundsAndDamage()
    {
        while (!isDestroyed)
        {
            if (!IsWithinBounds(transform.position))
            {
                TakeDamage(1);
                Debug.Log("Player is outside the background bounds and took damage.");
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }

    bool IsWithinBounds(Vector2 position)
    {
        return position.x >= minBounds.x && position.x <= maxBounds.x &&
               position.y >= minBounds.y && position.y <= maxBounds.y;
    }
}
