using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void GenerateStageOne()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);

        Debug.Log("num1 = " + num1);
        Debug.Log("num2 = " + num2);

        if (num1 > num2)
        {
            Debug.Log("'num1'is bigger than num2");
        }
        else if (num1 < num2)
        {
            Debug.Log("'num2'is bigger than num1");
        }
        else
        {
            Debug.Log("same num");
        }
    }
    public void GenerateStageTwo()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int userInput = 0 ;
        // num3 = num1 + num2
        int num3 = num1 + num2;
        if (num3 == userInput) // user Input()
        {
            print("right!");
        }
        else
        {
            print("wrong");
        }
    }
    public void GenerateStageThree()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num4 = Random.Range(0, 10);
        int num5 = Random.Range(0, 10);

        // num3 = num1 + num2
        int num3 = num1 + num2;
        // num6 = num4 + num5
        int num6 = num4 + num5;
        
        if (num3 > num6)
        {
            Debug.Log("'num3'is bigger than num6");
        }
        else if (num3 < num6)
        {
            Debug.Log("'num6'is bigger than num3");
        }
        else
        {
            Debug.Log("same num");
        }
    }
    public void GenerateStageFour()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);

        // num3 = num1 - num2
        int num3 = num1 - num2;

        Debug.Log("num1 = " + num1);
        Debug.Log(" num2 = " + num2);
        Debug.Log(" num3 = " + num3); 
    }
    public void GenerateStageFive()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num4 = Random.Range(0, 10);
        int num5 = Random.Range(0, 10);

        // num3 = num1 - num2
        int num3 = num1 - num2;
        // num6 = num4 - num5
        int num6 = num4 - num5;

        if (num3 > num6)
        {
            Debug.Log("'num3'�� �� Ů�ϴ�");
        }
        else if (num3 < num6)
        {
            Debug.Log("'num6'�� �� Ů�ϴ�.");
        }
        else
        {
            Debug.Log("�� ���� �����ϴ�.");
        }
    }
    public void GenerateStageSix()
    {
        int num1 = Random.Range(10, 100);
        int num2 = Random.Range(10, 100);

        // num3 = num1 + num2
        int num3 = num1 + num2;

    }
    public void GenerateStageSeven()
    {
        int num1 = Random.Range(10, 100);
        int num2 = Random.Range(10, 100);

        // num3 = num1 - num2
        int num3 = num1 - num2;
    }
}
