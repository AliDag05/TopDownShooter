using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Color _originalColor;
    private float _flashTime = 0.1f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    public void ResetColor()
    {
        StopAllCoroutines();
        _spriteRenderer.color = _originalColor;
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(_flashTime);
        _spriteRenderer.color = _originalColor;
    } 
}
