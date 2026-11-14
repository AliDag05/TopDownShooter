using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _spawnInterval = 2.0f;
    [SerializeField] private float _spawnRadius = 12.0f;

    private Transform _playerTransform;
    private IObjectPool<Enemy> _enemyPool;

    private int _activeEnemiesCount = 0;

    private void Awake()
    {
        if (_playerTransform == null)
        {
            _playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
        }
    }

    private void Start()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 15
            );

        InvokeRepeating(nameof(RequestEnemy), 1f, _spawnInterval);
    }

    private void RequestEnemy()
    {
        if (_activeEnemiesCount >= _maxEnemies) return;

        _enemyPool.Get();
    }

    Enemy CreateEnemy()
    {
        GameObject enemyObj = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity);

        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemy.SetPool(_enemyPool);

        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Sin(randomAngle);
        Vector3 spawnPosition = new Vector3(x * _spawnRadius + _playerTransform.position.x, y * _spawnRadius + _playerTransform.position.y, 0f);
        enemy.transform.position = spawnPosition;

        enemy.gameObject.SetActive(true);
        enemy.Init(_playerTransform);

        _activeEnemiesCount++;
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);

        _activeEnemiesCount--;
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        Destroy(enemy.gameObject);
    }
}
