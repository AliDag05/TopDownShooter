using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;

    private float _spawnInterval = 2.0f;
    
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8,8), Random.Range(-4,4), 0f);

        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount < 10)
        {
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void AddScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }
}
