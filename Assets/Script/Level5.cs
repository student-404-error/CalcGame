using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    private void Start()
    {
        // 1자리수 뼬셈의 대소비교

        // 0~9의 랜덤수
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);
        int Number4 = Random.Range(0, 10);
        int Number5 = Random.Range(0, 10);

        // Number3 = Number1 - Number2
        int Number3 = Number1 - Number2;
        // Number6 = Number4 - Number5
        int Number6 = Number4 - Number5;

        Debug.Log("첫번째 수: Number1 = " + Number1);
        Debug.Log("두번째 수: Number2 = " + Number2);
        Debug.Log("차 : Number3 = " + Number3);

        Debug.Log("세번째 수: Number4 = " + Number4);
        Debug.Log("네번째 수: Number5 = " + Number5);
        Debug.Log("차 : Number6 = " + Number6);

        if (Number3 > Number6)
        {
            Debug.Log("'Number3'이 더 큽니다");
        }
        else if (Number3 < Number6)
        {
            Debug.Log("'Number6'이 더 큽니다.");
        }
        else
        {
            Debug.Log("두 수가 같습니다.");
        }
    }
}
