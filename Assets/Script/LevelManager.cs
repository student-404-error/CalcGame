using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public GameObject popup;
    public GameObject CorrectNumberGauge;

    public TMP_Text problemText;
    public TMP_Text scoreText;
    public TMP_Text leftButtonText;
    public TMP_Text rightButtonText;
    public Button leftButton;
    public Button rightButton;
    public int playedRound = 0;
    public float accuracy = 0;

    private int num1;
    private int num2;
    private bool isRightSign;
    private char strictInequallity;
    private int score = 0;
    private bool correctAnswerBool;
    private int correctAnswerInt;
    public int levelNum;
    public float startTime;
    public float playTime;
    public float solvedSpeed;

    // 게임 종료 후 출력하는 텍스트
    public TMP_Text AccuracyText;
    public TMP_Text QuestionNumberText;
    public TMP_Text PlayTimeText;
    public TMP_Text AverageSolveTimeText;
    public TMP_Text CorrectNumberPercent;
    private GameManager mGameManager;
    private void Start()
    {
        popup.SetActive(false); // 팝업창 끄기
        startTime = Time.realtimeSinceStartup; // 시작시간 측정
        
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelNum = mGameManager.levelNum; // 임시로 레벨을 지정 (추후 스테이지 선택에서 값을 받을예정)

        switch (levelNum)
        {
            case 1: GenerateStageOne(); break;
            case 2: GenerateStageTwo(); break;
            case 3: GenerateStageThree(); break;
            case 4: GenerateStageFour(); break;
            case 5: GenerateStageFive(); break;
            case 6: GenerateStageSix(); break;
            case 7: GenerateStageSeven(); break;
            case 8: GenerateStageEight(); break;
            case 9: GenerateStageNine(); break;
            case 10: GenerateStageTen(); break;
            case 11: GenerateStageEleven(); break;
            case 12: GenerateStageTwelve(); break;
            case 13: GenerateStageThirteen(); break;
            case 14: GenerateStageFourteen(); break;
            default: Debug.LogError("Invalid level"); break;
        }
    }

    // 원형 그래프 채우는 애니메이션
    public IEnumerator AnimateGraph(float targetAccuracy, float duration)
    {
        float tempGraphPercent = 0;
        float percentage = 0;

        for (float time = duration; time > 0; time -= Time.deltaTime)
        {
            float deltaAmount = (1 - time * time * 4) * targetAccuracy;
            this.CorrectNumberGauge.GetComponent<Image>().fillAmount += deltaAmount - tempGraphPercent;
            percentage += (deltaAmount - tempGraphPercent) * 100;
            tempGraphPercent = deltaAmount; // 이전 프레임까지 늘어난 양

            
            CorrectNumberPercent.SetText(percentage.ToString("N0"));

            yield return null;
        }
    }

    // 끝나는 게임창을 보여주는 함수
    public void ShowEndResult()
    {
        playTime = Time.realtimeSinceStartup - startTime; // 끝나는 시간 측정

        if (score != 0)
        {
            accuracy = (float)score / playedRound * 100;
            accuracy = Mathf.Round((accuracy * 10f) / 10f);
        }
        
        solvedSpeed = ((float)playedRound / playTime) * 60; // 분당 푼 문제 수
        StartCoroutine(AnimateGraph(accuracy / 100, 0.5f));  

        AccuracyText.SetText("Accuracy\n" + accuracy.ToString("F1") + "%");    // 정확도
        QuestionNumberText.SetText("QuestionNumber\n" + playedRound);          // 문제 개수
        PlayTimeText.SetText("PlayTime\n" + playTime.ToString("N0") + "s");    // 플레이 시간
        AverageSolveTimeText.SetText("AverageSpeed\n" + solvedSpeed.ToString("N0") + "/min");   // 평균 풀이 시간

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
        switch (levelNum)
        {
            case 1: GenerateStageOne(); break;
            case 2: GenerateStageTwo(); break;
            case 3: GenerateStageThree(); break;
            case 4: GenerateStageFour(); break;
            case 5: GenerateStageFive(); break;
            case 6: GenerateStageSix(); break;
            case 7: GenerateStageSeven(); break;
            case 8: GenerateStageEight(); break;
            case 9: GenerateStageNine(); break;
            case 10: GenerateStageTen(); break;
            case 11: GenerateStageEleven(); break;
            case 12: GenerateStageTwelve(); break;
            case 13: GenerateStageThirteen(); break;
            case 14: GenerateStageFourteen(); break;
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

        if (isRightSign) // right is Correct
        {
            correctAnswerBool = true;
            leftButtonText.SetText(correctAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // left is Correct
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
        int num2 = num1 + 1; // 무조건 while문을 돌게 값을 설정

        while (num1 < num2)  // num1이 항상 크도록 num2 설정
        {
            num2 = Random.Range(0, 10);
        }
        

        correctAnswerInt = num1 - num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 10); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

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
    public void GenerateStageFive()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num3 = Random.Range(0, 10);
        int num4 = Random.Range(0, 10);

        int leftResult = num1 - num2;
        int rightResult = num3 - num4;

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
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " " + strictInequallity + " " + num3.ToString() + " - " + num4.ToString());
        leftButtonText.SetText("O");
        rightButtonText.SetText("X");
    }
    public void GenerateStageSix()
    {
        int num1 = Random.Range(10, 100);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(10, 100);
            if (num1 % 10 >= num2 % 10 && num1 > num2) break;
        }

        correctAnswerInt = num1 - num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 100); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

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
    public void GenerateStageSeven()
    {
        int num1 = Random.Range(10, 100);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(10, 100);
            if (num1 % 10 >= num2 % 10 && num1 > num2) break;
        }


        correctAnswerInt = num1 - num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 100); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

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
    public void GenerateStageEight()
    {
        int num1 = Random.Range(10, 100);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(10, 100);
            if (num1 % 10 >= num2 % 10 && num1 > num2) break;
        }
        int num3 = Random.Range(10, 100);
        int num4 = Random.Range(10, 100);

        int subtractResult = num1 - num2; // 두 수의 차를 저장
        int sumResult = num3 + num4;      // 두 수의 합을 저장

        isRightSign = Random.Range(0, 2) == 0;        // 부등호의 방향 결정
        strictInequallity = isRightSign ? '>' : '<';  // 부등호의 방향을 저장

        bool isRightOprator = Random.Range(0, 2) == 0;             // 연산자의 위치 설정
        int leftStrictInequallity = isRightOprator ? '+' : '-';  // 연산자의 종류를 저장
        int rightStrictInequallity = isRightOprator ? '-' : '+'; // 연산자의 종류를 저장

        // 출력할 부등호에 따를 O, X 정답을 correctAnswer에 저장
        if (isRightSign) // 오른쪽이 더 큰 부등호
        {
            if (isRightOprator) // 왼쪽이 + 라면
            {
                correctAnswerBool = sumResult > subtractResult;
                problemText.SetText(num3.ToString() + " + " + num4.ToString() + " " + strictInequallity + " " + num1.ToString() + " - " + num2.ToString());
            }
            else
            {
                correctAnswerBool = subtractResult > sumResult;
                problemText.SetText(num1.ToString() + " - " + num2.ToString() + " " + strictInequallity + " " + num3.ToString() + " + " + num4.ToString());
            }
        }
        else             // 왼쪽이 더 큰 부등호
        {
            if (isRightOprator) // 왼쪽이 + 라면
            {
                correctAnswerBool = sumResult < subtractResult;
                problemText.SetText(num3.ToString() + " + " + num4.ToString() + " " + strictInequallity + " " + num1.ToString() + " - " + num2.ToString());
            }
            else
            {
                correctAnswerBool = subtractResult < sumResult;
                problemText.SetText(num1.ToString() + " - " + num2.ToString() + " " + strictInequallity + " " + num3.ToString() + " + " + num4.ToString());
            }
        }

        leftButtonText.SetText("O");
        rightButtonText.SetText("X");
    }
    public void GenerateStageNine()
    {
        num1 = Random.Range(1, 10);
        num2 = Random.Range(1, 10);

        correctAnswerInt = num1 * num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(1, 82); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " * " + num2.ToString() + " =");

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
    public void GenerateStageTen()
    {
        int num1 = Random.Range(100, 1000);
        int num2 = Random.Range(100, 1000);

        correctAnswerInt = num1 + num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(200, 2000); // 잘못된 정답 만들기
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
    public void GenerateStageEleven()
    {
        int num1 = Random.Range(100, 1000);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(100, 1000);
            if (num1 % 10 >= num2 % 10 && (num1 / 10) % 10 >= (num2 / 10) % 10 && num1 > num2) break; // 받아내림 제거
        }

        correctAnswerInt = num1 - num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(100, 1000); // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

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
    public void GenerateStageTwelve()
    {
        num1 = Random.Range(10, 100);
        num2 = Random.Range(1, 10);

        correctAnswerInt = num1 * num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(1, 100) * 10 + correctAnswerInt % 10; // 잘못된 정답 만들기 (정답과 일의자리 수 일치)
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " * " + num2.ToString() + " =");

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
    public void GenerateStageThirteen()
    {
        int num1 = Random.Range(1000, 5000);
        int num2 = Random.Range(1000, 5000);

        correctAnswerInt = num1 + num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(100, 1000) * 10 + correctAnswerInt % 10; // 잘못된 정답 만들기 (정답과 일의자리 수 일치)
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
    public void GenerateStageFourteen()
    {
        int num1 = Random.Range(1000, 10000);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(1000, 10000);
            if (num1 % 10 >= num2 % 10 && (num1 / 10) % 10 >= (num2 / 10) % 10 && (num1 / 100) % 10 >= (num2 / 100) % 10 && num1 > num2) break; // 받아내림 제거
        }

        correctAnswerInt = num1 - num2;
        int wrongAnswer = correctAnswerInt;

        while (correctAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 900) * 10 + correctAnswerInt % 10; // 잘못된 정답 만들기
        }

        isRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

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
}
