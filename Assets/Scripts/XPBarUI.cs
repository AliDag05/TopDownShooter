using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{

    [SerializeField] private Image _fillImage;
    [SerializeField] private PlayerXP _playerXP;

    private void Awake()
    {
        if (_playerXP == null)
        {
            _playerXP = FindAnyObjectByType<PlayerXP>();
        }        
    }

    private void OnEnable()
    {
        if(_playerXP != null)
        {
            _playerXP.OnXPChanged += UpdateBar;
        }
    }

    private void OnDisable()
    {
        if (_playerXP != null)
        {
            _playerXP.OnXPChanged -= UpdateBar;
        }
    }

    private void UpdateBar(float currentXP, float targetXP)
    {
        if (_fillImage != null && targetXP > 0f)
        {
            _fillImage.fillAmount = currentXP / targetXP;
        }
    }
}
