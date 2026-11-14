using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
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
            _playerXP.OnLevelUp += UpdateLevelText;
        }
    }

    private void OnDisable()
    {
        if (_playerXP != null)
        {
            _playerXP.OnXPChanged -= UpdateBar;
            _playerXP.OnLevelUp -= UpdateLevelText;
        }
    }

    private void UpdateBar(float currentXP, float targetXP)
    {
        if (_fillImage != null && targetXP > 0f)
        {
            _fillImage.fillAmount = currentXP / targetXP;
        }
    }

    private void UpdateLevelText(int currentLevel)
    {
        if (_levelText != null)
        {
            _levelText.SetText("Level: {0}", currentLevel);
        }
    }
}
