using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private bool enableKeyboardControl = true;

    [SerializeField]
    private int maxHealth = 5;

    [SerializeField]
    private Text healthText;

    public HealthBar healthBar;
    public GameObject PlayerLaser;
    private int currentHealth;
    private Rigidbody2D rb2d;

    [SerializeField]
    private Animator animator;
    public GameObject Explosion;
    private bool isDestroyed = false;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float damageInterval = 1f; // Damage interval in seconds

    private float maxPlayerSpeed = 14f; // Maximal player speed
    private float laserSpeed = 15f; // Laser speed

    public GameOverScreen gameOver;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        UpdateHealthUI();
        CalculateBounds();
        StartCoroutine(CheckBoundsAndDamage());
    }

    void FixedUpdate()
    {
        if (isDestroyed)
            return;
        if (enableKeyboardControl)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
            rb2d.velocity = movement;
            float currentSpeed = movement.magnitude;
            animator.SetFloat("Speed", currentSpeed);
        }
        else
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb2d.MovePosition(mousePosition);
        }
    }

    void Update()
    {
        if (isDestroyed)
            return;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        float offsetDistance = 2.0f;
        GameObject laserInstance = Instantiate(
            PlayerLaser,
            transform.position + (transform.right * offsetDistance),
            transform.rotation
        );

        Rigidbody2D laserRb = laserInstance.GetComponent<Rigidbody2D>();
        if (laserRb != null)
        {
            laserRb.velocity = transform.right * laserSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Reticle") || collision.gameObject.CompareTag("Laser") )
            return;
        if (
            collision.gameObject.CompareTag("Alien")
            || collision.gameObject.CompareTag("Laser")
            || (collision.gameObject.CompareTag("Asteroid"))
        )
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
            healthBar.SetHealth(currentHealth);

            UpdateHealthUI();
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            isDestroyed = true;
            GameObject explosionInstance = Instantiate(
                Explosion,
                transform.position,
                transform.rotation
            );
            Destroy(gameObject);
            Destroy(explosionInstance, 1.0f);
            gameOver.Setup(ScoreManager.highScore);
            Time.timeScale = 0f;
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
        return position.x >= minBounds.x
            && position.x <= maxBounds.x
            && position.y >= minBounds.y
            && position.y <= maxBounds.y;
    }

    // Methods to handle upgrades
    public void UpgradeSpeed(float amount)
    {
        speed = Mathf.Min(speed + amount, maxPlayerSpeed);
        Debug.Log("Speed upgraded: " + speed);
    }

    public void UpgradeHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth; // Reset to full health after upgrading
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        UpdateHealthUI();
        Debug.Log("Health upgraded: " + maxHealth);
    }

    public void UpgradeLaser(float amount)
    {
        // Implement the laser upgrade logic
        Debug.Log("Laser upgraded by: " + amount);
    }
}
