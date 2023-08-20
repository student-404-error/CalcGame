using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
{
    private void Start()
    {
        // 2자리수 덧셈  

        // 10~99의 랜덤수
        int Number1 = Random.Range(10, 100);
        int Number2 = Random.Range(10, 100);

        // Number3 = Number1 + Number2
        int Number3 = Number1 + Number2;

        Debug.Log("첫번째 수: Number1 = " + Number1);
        Debug.Log("두번째 수: Number2 = " + Number2);
        Debug.Log("합 : Number3 = " + Number3);

        //return Number3;
    }
}
