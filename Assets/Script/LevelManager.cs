using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public TMP_Text problemText;
    public TMP_Text scoreText;
    public TMP_Text leftButtonText;
    public TMP_Text rightButtonText;
    public Button leftButton;
    public Button rightButton;
    private int playedRound;
    private int num1;
    private int num2;
    private bool isRightSign;
    private char strictInequallity;
    private int score = 0;
    private bool correctAnswerBool;
    private int correctAnswerInt;
    private GameManager mGameManager;
    private int levelNumber;
    private void Start()
    {
        
        levelNumber = mGameManager.levelNum; // 임시로 레벨을 지정 (추후 스테이지 선택에서 값을 받을예정)

        switch (levelNumber)
        {
            case 1: GenerateStageOne(); break;
            case 2: GenerateStageTwo(); break;
            case 3: GenerateStageThree(); break;
            case 4: GenerateStageFour(); break;
            case 5: GenerateStageFive(); break;
            case 6: GenerateStageSix(); break;
            case 7: GenerateStageSeven(); break;
                /*
            case 8: GenerateStageEight(); break;
            case 9: GenerateStageNine(); break;
            case 10: GenerateStageTen(); break;
            case 11: GenerateStageEleven(); break;
            case 12: GenerateStageTwelve(); break;
            case 13: GenerateStageThirteen(); break;
            case 14: GenerateStageFourteen(); break;
                */
            default: Debug.LogError("Invalid level"); break;
        }
    }

    public void CheckAnswer(bool playerChoice)
    {
        playedRound++; // 플레이 횟수 증가
        if (playerChoice == correctAnswerBool)
        {
            Debug.Log("Correct!");
            // 정답일 때 실행할 코드를 여기에 추가
            score++;
            scoreText.SetText("Score : " + score);
        }
        else
        {
            Debug.Log("Wrong!");
            // 오답일 때 실행할 코드를 여기에 추가

        }

        // 새 문제 생성
        switch (levelNumber)
        {
            case 1: GenerateStageOne(); break;
            case 2: GenerateStageTwo(); break;
            case 3: GenerateStageThree(); break;
            case 4: GenerateStageFour(); break;
            case 5: GenerateStageFive(); break;
            case 6: GenerateStageSix(); break;
            case 7: GenerateStageSeven(); break;
            /*
        case 8: GenerateStageEight(); break;
        case 9: GenerateStageNine(); break;
        case 10: GenerateStageTen(); break;
        case 11: GenerateStageEleven(); break;
        case 12: GenerateStageTwelve(); break;
        case 13: GenerateStageThirteen(); break;
        case 14: GenerateStageFourteen(); break;
            */
            default: Debug.LogError("Invalid level"); break;
        }
    }

    public void GenerateStageOne()
    {
        num1 = Random.Range(0, 10);
        num2 = Random.Range(0, 10);

        isRightSign = Random.Range(0, 2) == 0;        // 부등호의 방향 결정
        strictInequallity = isRightSign ? '>' : '<';  // 부등호의 방향을 저장

        // 출력할 부등호에 따를 O, X 정답을 correctAnswer에 저장
        if (isRightSign)
        {
            correctAnswerBool = num1 > num2;
        }
        else
        {
            correctAnswerBool = num1 < num2;
        }

        // 문제를 출력
        problemText.SetText(num1.ToString() + " " + strictInequallity + " " + num2.ToString());
        leftButtonText.SetText("O");
        rightButtonText.SetText("X");
    }
    public void GenerateStageTwo()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);

        correctAnswerInt = num1 + num2;
        int wrongAnswer = correctAnswerInt;

        while(correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 20); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + " =");

        if (isRightSign) // 정답이 오른쪽
        {
            correctAnswerBool = true;
            leftButtonText.SetText(correctAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            correctAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(correctAnswerInt.ToString());
        }
    }
    public void GenerateStageThree()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num3 = Random.Range(0, 10);
        int num4 = Random.Range(0, 10);

        int leftResult = num1 + num2;
        int rightResult = num3 + num4;

        isRightSign = Random.Range(0, 2) == 0;        // 부등호의 방향 결정
        strictInequallity = isRightSign ? '>' : '<';  // 부등호의 방향을 저장

        // 출력할 부등호에 따를 O, X 정답을 correctAnswer에 저장
        if (isRightSign)
        {
            correctAnswerBool = leftResult > rightResult;
        }
        else
        {
            correctAnswerBool = leftResult < rightResult;
        }

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + " " + strictInequallity + " " + num3.ToString() + " + " + num4.ToString());
        leftButtonText.SetText("O");
        rightButtonText.SetText("X");
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
