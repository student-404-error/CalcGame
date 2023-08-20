using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    private void Start()
    {
        // 1자리수 뻴셈  

        // 0~9의 랜덤수
        int Number1 = Random.Range(0, 10);
        int Number2 = Random.Range(0, 10);

        // Number3 = Number1 - Number2
        int Number3 = Number1 - Number2;

        Debug.Log("첫번째 수: Number1 = " + Number1);
        Debug.Log("두번째 수: Number2 = " + Number2);
        Debug.Log("차 : Number3 = " + Number3);

        //return Number3;
    }
}