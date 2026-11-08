using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    
    private Transform _playerTransform;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform playerTransform)
    {
        _playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_playerTransform.position - transform.position);

        if (direction.sqrMagnitude < 0.15f)
        {
            Destroy(gameObject);
            return;
        }

        _rb.linearVelocity = direction.normalized * _speed;
    }
}
