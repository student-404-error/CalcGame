using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    private void Start()
    {
        // 1�ڸ��� �y��  

        // 0~9�� ������
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);

        // Number3 = Number1 - Number2
        int Number3 = Number1 - Number2;

        Debug.Log("ù��° ��: Number1 = " + Number1);
        Debug.Log("�ι�° ��: Number2 = " + Number2);
        Debug.Log("�� : Number3 = " + Number3);

        //return Number3;
    }
}