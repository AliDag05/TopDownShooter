using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _spawnInterval = 2.0f;

    private IObjectPool<Enemy> _enemyPool;

    private int _activeEnemiesCount = 0;

    void Start()
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

    void RequestEnemy()
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

    void OnGetEnemy(Enemy enemy)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0f);
        enemy.transform.position = spawnPosition;

        enemy.gameObject.SetActive(true);
        enemy.Init(_playerTransform);
        enemy.OnDeath += HandleEnemyDeath;

        _activeEnemiesCount++;
    }

    void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);

        enemy.OnDeath -= HandleEnemyDeath;

        _activeEnemiesCount--;
    }

    void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    void HandleEnemyDeath(Enemy enemy)
    {
        if (_gameManager != null)
        {
            _gameManager.AddScore();
        }
    }
}
