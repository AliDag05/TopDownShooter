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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null) { player.TakeDamage(1); }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        
        if (_health <= 0) 
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null ) { gameManager.AddScore(); }
            
            Destroy(gameObject);
        }
    }
}
