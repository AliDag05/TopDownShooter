using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.2f;

    private float _bulletSpeed = 10.0f;
    private float _nextFireTime ;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && _nextFireTime <= Time.time)
        //{
        //    ShootBullet();
        //    _nextFireTime = Time.time + _fireRate;
        //}

        if (Gamepad.current != null)
        {
            Vector2 shootDir = Gamepad.current.rightStick.ReadValue();

            if (shootDir.magnitude > 0.1f && Time.time >= _nextFireTime)
            {
                ShootBullet(shootDir);

                _nextFireTime = Time.time + _fireRate;
            }
        }
    }

    void ShootBullet(Vector2 direction)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 shootDirection = (mouseWorldPosition - transform.position);
        
        GameObject spawnedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = direction.normalized * _bulletSpeed;
        }
    }
}
