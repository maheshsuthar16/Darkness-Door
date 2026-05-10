using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 8f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = moveDirection * bulletSpeed; // apply immediately

    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        if (rb != null) rb.linearVelocity = moveDirection * bulletSpeed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet Trigger Hit: " + other.name);

        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.AddScore(50);
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet Collision Hit: " + collision.collider.name);
        Destroy(gameObject);
    }
}