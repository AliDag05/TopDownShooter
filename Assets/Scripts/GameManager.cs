using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private GameObject _gameOverPanel;

    private int _score = 0;

    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void AddScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
