using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void Start()
    {
        // 1�ڸ��� ��Һ�

        // 0~9�� ������
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);

        Debug.Log("ù��° ��: Number1 = " + Number1);
        Debug.Log("�ι�° ��: Number2 = " + Number2);

        if (Number1 > Number2)
        {
            Debug.Log("'Number1'�� �� Ů�ϴ�");
        }
        else if (Number1 < Number2)
        {
            Debug.Log("'Number2'�� �� Ů�ϴ�.");
        }
        else
        {
            Debug.Log("�� ���� �����ϴ�.");
        }
    }
}
