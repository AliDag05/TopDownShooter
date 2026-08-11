using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null ) { enemy.TakeDamage(1); Destroy(gameObject); }

        if (collision.name.Contains("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
