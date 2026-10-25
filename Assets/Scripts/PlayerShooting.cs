using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate = 0.2f;

    private IObjectPool<Bullet> _bulletPool;

    private float _bulletSpeed = 10.0f;
    private float _nextFireTime ;

    private void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 50
        );
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && _nextFireTime <= Time.time)
        //{
        //    ShootBullet();
        //    _nextFireTime = Time.time + _fireRate;
        //}

        Vector2 shootDir = Vector2.zero;

        if (Gamepad.current != null)
        {
            Vector2 stickInput = Gamepad.current.rightStick.ReadValue();

            if (stickInput.magnitude > 0.1f)
            {
                shootDir = stickInput;

                float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }

        if (shootDir == Vector2.zero && Mouse.current != null)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.nearClipPlane));

            Vector2 lookDir = mouseWorldPos - (Vector2)transform.position;

            if (lookDir != Vector2.zero)
            {
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            if (Mouse.current.leftButton.isPressed)
            {
                shootDir = lookDir;
            }

        }

        if (shootDir != Vector2.zero && Time.time >= _nextFireTime)
        {
            ShootBullet(shootDir);
            _nextFireTime = Time.time + _fireRate;
        }
    }

    Bullet CreateBullet()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab);

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.SetPool(_bulletPool);

        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.ResetBulletState();
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ShootBullet(Vector2 direction)
    {
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 shootDirection = (mouseWorldPosition - transform.position);

        //GameObject spawnedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        //Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        
        Bullet bullet = _bulletPool.Get();

        bullet.transform.position = transform.position;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = direction.normalized * _bulletSpeed;
        }
    }
}
