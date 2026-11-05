using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _lookDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //_moveInput.x = Input.GetAxisRaw("Horizontal");
        //_moveInput.y = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) moveInput.y = 1f;
            if (Keyboard.current.sKey.isPressed) moveInput.y = -1f;
            if (Keyboard.current.aKey.isPressed) moveInput.x = -1f;
            if (Keyboard.current.dKey.isPressed) moveInput.x = 1f;
        }

        if (Gamepad.current != null) 
        { 
            _moveInput = Gamepad.current.leftStick.ReadValue(); 
            _lookDir = Gamepad.current.rightStick.ReadValue(); 
        }

        if (moveInput == Vector2.zero && Gamepad.current != null)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();
        }

        _moveInput = moveInput;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        //_lookDir = mousePos - transform.position;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput.normalized * _speed;
    }
}
