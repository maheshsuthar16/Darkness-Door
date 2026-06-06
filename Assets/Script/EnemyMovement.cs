using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   

    [SerializeField] float  moveSpeed = 5f; 
    Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
      rigidbody.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D (Collider2D  collision)
    {
        moveSpeed = - moveSpeed;
        FlipEnemyFacing();

    }
   
   void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody.linearVelocity.x)), 1f);
    }

}
