using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickScript : MonoBehaviour
{
    public LevelManager levelManager;

    public void Start()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

    public void OnLeftButtonClicked() // 왼쪽 버튼을 눌렀는지
    {
        levelManager.CheckAnswer(true);
    }

    public void OnRightButtonClicked() // 오른쪽 버튼을 눌렀는지
    {
        levelManager.CheckAnswer(false);
    }
}
