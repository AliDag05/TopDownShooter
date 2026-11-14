using System;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private float _targetXP = 10.0f;
    [SerializeField] private float _xpMultiplier = 1.5f;

    private float _currentXP;    
    private int _currentLevel = 1;

    public event Action<float, float> OnXPChanged;
    public event Action<int> OnLevelUp;

    private void Start()
    {
        OnLevelUp?.Invoke(_currentLevel);
        OnXPChanged?.Invoke(_currentXP, _targetXP);
    }

    public void AddXP(int amount)
    {
        if (amount <= 0 || _targetXP <= 0f) return;

        _currentXP += amount;
        
        while(_currentXP >= _targetXP)
        {
            _currentXP -= _targetXP;
            _currentLevel++;

            _targetXP *= _xpMultiplier;
            OnLevelUp?.Invoke(_currentLevel);
        }

        OnXPChanged?.Invoke(_currentXP, _targetXP);
    }
}
