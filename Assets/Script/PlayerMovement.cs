using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbspeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 8f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    public static PlayerMovement Instance;
    Vector2 moveInput;
    Rigidbody2D rigidbody;
    Animator animator;
    CapsuleCollider2D mycollider;
    BoxCollider2D myfeetCollider;
    float gravityScaleatStart;
    bool isAlive = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mycollider = GetComponent<CapsuleCollider2D>();
        myfeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleatStart = 8f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimberLadder();
        Die();

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidbody.linearVelocity.y);
        rigidbody.linearVelocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(rigidbody.linearVelocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", hasHorizontalSpeed);

    }
    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rigidbody.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.linearVelocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if (!myfeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (!isAlive) return;
        if (value.isPressed)
        {
            rigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        }


    }

    void ClimberLadder()
    {
        if (!myfeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidbody.gravityScale = gravityScaleatStart;
            animator.SetBool("IsClimbing", false);
            return;
        }
        rigidbody.gravityScale = 0f;
        Vector2 ClimberVelocity = new Vector2(rigidbody.linearVelocity.x, moveInput.y * climbspeed);
        rigidbody.linearVelocity = ClimberVelocity;
        bool hasVerticleSpeed = Mathf.Abs(rigidbody.linearVelocity.y) > Mathf.Epsilon;
        animator.SetBool("IsClimbing", hasVerticleSpeed);
    }

    void Die()
    {
        if (mycollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("dying");
            rigidbody.linearVelocity = deathKick;
            GameManager.Instance.Lose();

        }
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive) return;
        if (value.isPressed)
        {
            GameObject newBullet = Instantiate(bullet, gun.position, transform.rotation);

            // Get the BulletMovement component
            BulletMovement bulletMovement = newBullet.GetComponent<BulletMovement>();

            // Pass the direction based on player facing
            Vector2 direction = new Vector2(transform.localScale.x, 0f);
            bulletMovement.SetDirection(direction);
        }
    }
    public void Revive()
    {
        isAlive = true;
        rigidbody.linearVelocity = Vector2.zero;
        animator.ResetTrigger("dying");
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsClimbing", false);
        rigidbody.gravityScale = gravityScaleatStart;
    }
}
