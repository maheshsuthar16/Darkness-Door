using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.AddScore(50);
            Destroy(gameObject);
        }
    }
}