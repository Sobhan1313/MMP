using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool enableKeyboardControl;
    private Rigidbody2D rb2d;
    private Collision2D c2D;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

    void OnCollisionEnter2D(Collision2D c2D)
    {
        Debug.Log("Ship go boom");
    }
}