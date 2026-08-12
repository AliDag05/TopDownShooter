using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private float _spawnInterval = 2.0f;
    
    void Start()
    {
        InvokeRepeating("Spawner", 1f, _spawnInterval);
    }

    
    void Update()
    {
        
    }

    void Spawner()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
}
