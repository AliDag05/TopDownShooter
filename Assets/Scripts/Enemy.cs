using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _health = 3;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    Vector2 lookDir;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null )
        {
            _playerTransform = playerObj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            lookDir = (_playerTransform.position - transform.position).normalized;
            _rb.linearVelocity = lookDir * _speed;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0) { Destroy(gameObject); }
    }
}
