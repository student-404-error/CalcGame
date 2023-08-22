using UnityEngine;
using UnityEngine.UI;

public class ResizeUIElement : MonoBehaviour
{
    private RectTransform rectTransform;
    public float widthRatio = 0.5f;
    public float heightRatio = 0.5f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeToScreenSize();
    }

    public void ResizeToScreenSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float newWidth = screenWidth * widthRatio; 
        float newHeight = screenHeight * heightRatio;

        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
