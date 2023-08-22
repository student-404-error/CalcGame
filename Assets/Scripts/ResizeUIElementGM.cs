using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResizeUIElementGM : MonoBehaviour
{
    private RectTransform rectTransform;

    // ������Ʈ�� ȭ�� ũ�� �������� ����
    public float widthRatio = 0.5f;
    public float heightRatio = 0.5f;

    // ������Ʈ ũ�� ���ؿ��� �߽ɺ��� �Ÿ� ����
    public float blankX = 0.5f;
    public float blankY = 0.5f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeToScreenSize();

        if (rectTransform != null)
        {
            Vector2 size = rectTransform.sizeDelta; // ������Ʈ�� ���μ��� �� ��������
            float width = size.x;
            float height = size.y;

            Vector2 offsetFromCenter = new Vector2(blankX * width, blankY * height);

            Vector2 newPosition = offsetFromCenter; // ��ġ ����

            rectTransform.anchoredPosition = newPosition;
        }

        float newFontSize = rectTransform.sizeDelta.y / 2.5f; // ���ϴ� �� ��Ʈ ũ��

        //��Ʈ ������ ����
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
