using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    private void Start()
    {
        // 1�ڸ��� ������ ��Һ�

        // 0~9�� ������
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);
        int Number4 = Random.Range(0, 10);
        int Number5 = Random.Range(0, 10);

        // Number3 = Number1 - Number2
        int Number3 = Number1 - Number2;
        // Number6 = Number4 - Number5
        int Number6 = Number4 - Number5;

        Debug.Log("ù��° ��: Number1 = " + Number1);
        Debug.Log("�ι�° ��: Number2 = " + Number2);
        Debug.Log("�� : Number3 = " + Number3);

        Debug.Log("����° ��: Number4 = " + Number4);
        Debug.Log("�׹�° ��: Number5 = " + Number5);
        Debug.Log("�� : Number6 = " + Number6);

        if (Number3 > Number6)
        {
            Debug.Log("'Number3'�� �� Ů�ϴ�");
        }
        else if (Number3 < Number6)
        {
            Debug.Log("'Number6'�� �� Ů�ϴ�.");
        }
        else
        {
            Debug.Log("�� ���� �����ϴ�.");
        }
    }
}
