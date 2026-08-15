using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _health = 3;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _lookDir;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _healthText.text = "Health: " + _health;
    }

    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        _lookDir = mousePos - transform.position;
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput.normalized * _speed;
        float angle = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg; 
        _rb.rotation = angle;
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        _healthText.text = "Health: " + _health;
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (_health <= 0) 
        {
            gameManager.GameOver();
            Destroy(gameObject); 
        }
    }
}
