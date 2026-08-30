using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> _pool;

    private Rigidbody2D _rb;
    private bool _isReleased;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ResetBulletState()
    {
        _rb.linearVelocity = Vector2.zero;
        _isReleased = false;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReleased) return;

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null ) { enemy.TakeDamage(1); ReleaseToPool(); }

        if (collision.name.Contains("Wall"))
        {
            ReleaseToPool();
        }
    }

    private void ReleaseToPool()
    {
        _isReleased = true;
        _pool.Release(this);
    }
}
