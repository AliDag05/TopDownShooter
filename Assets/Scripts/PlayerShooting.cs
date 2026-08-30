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

    void Start()
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

    Bullet CreateBullet()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab);

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.SetPool(_bulletPool);

        return bullet;
    }

    void OnGetBullet(Bullet bullet)
    {
        bullet.ResetBulletState();
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void ShootBullet(Vector2 direction)
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
