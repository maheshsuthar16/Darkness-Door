using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float lifetime = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
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
          other.GetComponent<Enemy>()?.TakeDamage(1);
        }
        
        Destroy(gameObject);
  
    }


    
}