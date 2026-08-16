using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        Rect safeArea = Screen.safeArea;

        float minX = safeArea.x / Screen.width;
        float minY = safeArea.y / Screen.height;
        Vector2 anchorMin = new Vector2(minX, minY);

        float maxX = (safeArea.x + safeArea.width)/ Screen.width;
        float maxY = (safeArea.y + safeArea.height) / Screen.height;
        Vector2 anchorMax = new Vector2(maxX, maxY);

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
