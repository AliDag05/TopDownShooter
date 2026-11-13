using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _xpReward = 2;

    private PlayerXP _playerXP;
    private Transform _playerTransform;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        if (_playerTransform != null )
        {
            _playerTransform.TryGetComponent(out _playerXP);
        }        
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null) return;

        Vector2 direction = (_playerTransform.position - transform.position);

        if (direction.sqrMagnitude < 0.15f)
        {
            if (_playerXP != null) _playerXP.AddXP(_xpReward);
            Destroy(gameObject);
            return;
        }

        _rb.linearVelocity = direction.normalized * _speed;
    }
}
