using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _spawnInterval = 2.0f;

    private int _activeEnemiesCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
    }

    void SpawnEnemy()
    {
        if (_activeEnemiesCount >= _maxEnemies) return;

        Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0f);

        GameObject enemyObj = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemy.Init(_playerTransform);

        enemy.OnDeath += HandleEnemyDeath;

        _activeEnemiesCount++;
    }

    void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;

        _activeEnemiesCount--;

        if (_gameManager != null)
        {
            _gameManager.AddScore();
        }
    }
}
