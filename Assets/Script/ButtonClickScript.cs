using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickScript : MonoBehaviour
{
    public LevelManager levelManager;

    public void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public void OnLeftButtonClicked() // ���� ��ư�� ��������
    {
        levelManager.CheckAnswer(true);
    }

    public void OnRightButtonClicked() // ������ ��ư�� ��������
    {
        levelManager.CheckAnswer(false);
    }
}
