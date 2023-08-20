using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void Start()
    {
        // 1자리수 대소비교

        // 0~9의 랜덤수
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);

        Debug.Log("첫번째 수: Number1 = " + Number1);
        Debug.Log("두번째 수: Number2 = " + Number2);

        if (Number1 > Number2)
        {
            Debug.Log("'Number1'이 더 큽니다");
        }
        else if (Number1 < Number2)
        {
            Debug.Log("'Number2'가 더 큽니다.");
        }
        else
        {
            Debug.Log("두 수가 같습니다.");
        }
    }
}
