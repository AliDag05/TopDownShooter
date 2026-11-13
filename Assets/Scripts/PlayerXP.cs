using System;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private float _targetXP = 10.0f;
    private float _currentXP;    
    private int _currentLevel = 1;

    public event Action<float, float> OnXPChanged;

    private void Start()
    {
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
        }

        OnXPChanged?.Invoke(_currentXP, _targetXP);
    }
}
