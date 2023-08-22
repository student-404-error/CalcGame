using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResizeUIElementGM : MonoBehaviour
{
    private RectTransform rectTransform;

    // 오브젝트를 화면 크기 기준으로 설정
    public float widthRatio = 0.5f;
    public float heightRatio = 0.5f;

    // 오브젝트 크기 기준에서 중심부터 거리 설정
    public float blankX = 0.5f;
    public float blankY = 0.5f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeToScreenSize();

        if (rectTransform != null)
        {
            Vector2 size = rectTransform.sizeDelta; // 오브젝트의 가로세로 값 가져오기
            float width = size.x;
            float height = size.y;

            Vector2 offsetFromCenter = new Vector2(blankX * width, blankY * height);

            Vector2 newPosition = offsetFromCenter; // 위치 설정

            rectTransform.anchoredPosition = newPosition;
        }

        float newFontSize = rectTransform.sizeDelta.y / 2.5f; // 원하는 새 폰트 크기

        //폰트 사이즈 설정
        TextMeshProUGUI tmpText = GetComponent<TextMeshProUGUI>();

        if (tmpText != null)
        {
            tmpText.fontSize = newFontSize;
        }
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
