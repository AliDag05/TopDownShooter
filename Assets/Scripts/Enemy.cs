using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _health = 3;

    public event Action<Enemy> OnDeath;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private DamageFlash _damageFlash;
    private float _currentHealth;
    Vector2 lookDir;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _damageFlash = GetComponent<DamageFlash>();
    }

    public void Init(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _currentHealth = _health;
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            lookDir = (_playerTransform.position - transform.position).normalized;
            _rb.linearVelocity = lookDir * _speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null) { player.TakeDamage(1); }
    }

    public void TakeDamage(int damageAmount)
    {
        _damageFlash?.Flash();

        _currentHealth -= damageAmount;
        
        if (_currentHealth <= 0) 
        {
            OnDeath?.Invoke(this);
            
            Destroy(gameObject);
        }
    }
}
