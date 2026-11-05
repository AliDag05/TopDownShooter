using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _invulnerabilityDuration = 0.5f;
    [SerializeField] private TextMeshProUGUI _healthText;

    private DamageFlash _damageFlash;

    private bool _isInvulnerable;

    private void Awake()
    {
        _damageFlash = GetComponent<DamageFlash>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;

        if (_healthText != null)
        {
            _healthText.text = "Health: " + _currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (_isInvulnerable) { return; }

        _currentHealth -= damage;

        if (_healthText != null)
        {
            _healthText.text = "Health: " + _currentHealth;
        }

        if (_currentHealth <= 0)
        {
            FindAnyObjectByType<GameManager>()?.GameOver();
            Destroy(gameObject);
            return;
        }

        StartCoroutine(BecomeInvolnerable());
    }

    private IEnumerator BecomeInvolnerable()
    {
        _isInvulnerable = true;

        float elapsed = 0.1f;

        while (elapsed < _invulnerabilityDuration)
        {
            _damageFlash?.Flash();

            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }

        _isInvulnerable = false;
    }
}
