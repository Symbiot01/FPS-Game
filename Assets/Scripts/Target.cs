using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public int points = 10;  // Points awarded for destroying this target
    private TargetSpawner targetSpawner;

    void Start()
    {
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (targetSpawner != null)
        {
            targetSpawner.TargetDestroyed(gameObject);
        }
        
        GameManager.Instance.AddPoints(points);  // Award points to the player
        Destroy(gameObject);
    }
}