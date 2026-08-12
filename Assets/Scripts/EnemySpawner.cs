using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private float _spawnInterval = 2.0f;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
    }

    
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8,8), Random.Range(-4,4), 0f);

        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
