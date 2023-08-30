using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public GameObject popup;
    public GameObject correctNumberGauge;
    public GameObject explainImageChoose;
    public GameObject explainImageCalc;
    public GameObject goodFace;
    public GameObject badFace;
    
    public TMP_Text problemText;
    public TMP_Text leftButtonText;
    public TMP_Text rightButtonText;
    public TMP_Text levelText;
    public TMP_Text scoreText;
    public Button leftButton;
    public Button rightButton;
    public AudioClip correctSound; // 정답 효과음

    public int playedRound = 0;
    public float accuracy = 0;
    private bool isFeedbackShowing = false;    
    // private int num1;
    // private int num2;
    private bool mIsRightSign;
    private char mStrictInequality;
    public int score = 0;
    private bool mCorrectAnswerBool;
    private int mCorrectAnswerInt;
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
        goodFace.SetActive(false);
        badFace.SetActive(false);
        
        startTime = Time.realtimeSinceStartup; // 시작시간 측정
        
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelNum = mGameManager.levelNum; // 임시로 레벨을 지정 (추후 스테이지 선택에서 값을 받을예정)
        levelText.SetText(levelNum.ToString()); // 현재 레벨 표시
        
        if (levelNum is 1 or 3 or 6) // 대소비교 연산 게임인 경우
        {
            explainImageChoose.SetActive(true);
            explainImageCalc.SetActive(false);
        }
        else
        {
            explainImageChoose.SetActive(false);
            explainImageCalc.SetActive(true);
        }
        
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
    private IEnumerator AnimateGraph(float targetAccuracy, float duration)
    {
        float tempGraphPercent = 0;
        float percentage = 0;

        for (float time = duration; time > 0; time -= Time.deltaTime)
        {
            float deltaAmount = (1 - time * time * 4) * targetAccuracy;
            this.correctNumberGauge.GetComponent<Image>().fillAmount += deltaAmount - tempGraphPercent;
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

        AccuracyText.SetText("Accuracy\n" + accuracy.ToString("F1") + "%");                     // 정확도
        QuestionNumberText.SetText("QuestionNumber\n" + playedRound);                                 // 문제 개수
        PlayTimeText.SetText("PlayTime\n" + playTime.ToString("N0") + "s");                     // 플레이 시간
        AverageSolveTimeText.SetText("AverageSpeed\n" + solvedSpeed.ToString("N0") + "/min");   // 평균 풀이 시간

    }
    public void CheckAnswer(bool playerChoice)
    {
        playedRound++; // 플레이 횟수 증가
        if (playerChoice == mCorrectAnswerBool)
        {
            Debug.Log("Correct!");
            // 정답일 때 실행할 코드를 여기에 추가
            score++;
            scoreText.SetText(score.ToString());
        }
        else
        {
            Debug.Log("Wrong!");
            // 오답일 때 실행할 코드를 여기에 추가
        }

        // 반응 화면 출력
        ShowFeedback(playerChoice == mCorrectAnswerBool);
    }

    private void ShowFeedback(bool isCorrect)
    {
        if (isFeedbackShowing) return; // 이미 피드백 중이면 중복 실행 방지
        
        isFeedbackShowing = true; // 피드백 표시 중임을 표시
        
        StartCoroutine(DisplayFeedback(isCorrect));
    }
    private IEnumerator DisplayFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            goodFace.SetActive(true);
        }
        else
        {
            badFace.SetActive(true);
        }
        
        // 효과음 재생
        AudioSource.PlayClipAtPoint(correctSound, transform.position);
        
        // 일정 시간(예: 2초) 동안 대기
        yield return new WaitForSecondsRealtime(1f);

        // 이모티콘 제거 및 다음 문제 생성
        if (isCorrect)
        {
            goodFace.SetActive(false);
        }
        else
        {
            badFace.SetActive(false);
        }

        isFeedbackShowing = false; // 피드백 종료
        CreateNewQuestion();
    }
    private void CreateNewQuestion()
    {
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
    private void GenerateStageOne()
    {
        int num1 = Random.Range(0, 10);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(0, 10);
            if (num1 != num2) break;
        }
        
        mCorrectAnswerBool = num1 > num2;

        // 문제를 출력
        problemText.SetText(num1.ToString() + "     " + num2.ToString());
        leftButtonText.SetText(num1.ToString());
        rightButtonText.SetText(num2.ToString());
    }
    private void GenerateStageTwo()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);

        mCorrectAnswerInt = num1 + num2;
        int wrongAnswer = mCorrectAnswerInt;

        while(mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 20); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + " =");

        if (mIsRightSign) // right is Correct
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // left is Correct
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageThree()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num3 = num1;
        int num4 = num2;
        while (true)
        {
            num3 = Random.Range(0, 10);
            num4 = Random.Range(0, 10);
            if (num1 + num2 != num3 + num4) break;
        }

        int leftResult = num1 + num2;
        int rightResult = num3 + num4;

        mCorrectAnswerBool = leftResult > rightResult;

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + "     " + num3.ToString() + " + " + num4.ToString());
        leftButtonText.SetText(num1.ToString() + " + " + num2.ToString());
        rightButtonText.SetText(num3.ToString() + " + " + num4.ToString());
    }
    private void GenerateStageFour()
    {
        int num1 = Random.Range(0, 10);
        int num2 = num1 + 1; // 무조건 while문을 돌게 값을 설정

        while (num1 < num2)  // num1이 항상 크도록 num2 설정
        {
            num2 = Random.Range(0, 10);
        }
        

        mCorrectAnswerInt = num1 - num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 10); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageFive()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num3 = num1;
        int num4 = num2;
        while (true)
        {
            num3 = Random.Range(0, 10);
            num4 = Random.Range(0, 10);
            if (num1 - num2 != num3 - num4) break;
        }

        int leftResult = num1 - num2;
        int rightResult = num3 - num4;

        mCorrectAnswerBool = leftResult > rightResult;

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + "     " + num3.ToString() + " - " + num4.ToString());
        leftButtonText.SetText(num1.ToString() + " - " + num2.ToString());
        rightButtonText.SetText(num3.ToString() + " - " + num4.ToString());
    }
    private void GenerateStageSix()
    {
        int num1 = Random.Range(10, 100);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(10, 100);
            if (num1 % 10 >= num2 % 10 && num1 > num2) break;
        }

        mCorrectAnswerInt = num1 - num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 100); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }

    }
    private void GenerateStageSeven()
    {
        int num1 = Random.Range(10, 100);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(10, 100);
            if (num1 % 10 >= num2 % 10 && num1 > num2) break;
        }


        mCorrectAnswerInt = num1 - num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 100); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageEight()
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

        mIsRightSign = Random.Range(0, 2) == 0;        // 부등호의 방향 결정
        mStrictInequality = mIsRightSign ? '>' : '<';  // 부등호의 방향을 저장

        bool isRightOperator = Random.Range(0, 2) == 0;             // 연산자의 위치 설정
        int leftStrictInequality = isRightOperator ? '+' : '-';  // 연산자의 종류를 저장
        int rightStrictInequality = isRightOperator ? '-' : '+'; // 연산자의 종류를 저장

        // 출력할 부등호에 따를 O, X 정답을 correctAnswer에 저장
        if (mIsRightSign) // 오른쪽이 더 큰 부등호
        {
            if (isRightOperator) // 왼쪽이 + 라면
            {
                mCorrectAnswerBool = sumResult > subtractResult;
                problemText.SetText(num3.ToString() + " + " + num4.ToString() + " " + mStrictInequality + " " + num1.ToString() + " - " + num2.ToString());
            }
            else
            {
                mCorrectAnswerBool = subtractResult > sumResult;
                problemText.SetText(num1.ToString() + " - " + num2.ToString() + " " + mStrictInequality + " " + num3.ToString() + " + " + num4.ToString());
            }
        }
        else             // 왼쪽이 더 큰 부등호
        {
            if (isRightOperator) // 왼쪽이 + 라면
            {
                mCorrectAnswerBool = sumResult < subtractResult;
                problemText.SetText(num3.ToString() + " + " + num4.ToString() + " " + mStrictInequality + " " + num1.ToString() + " - " + num2.ToString());
            }
            else
            {
                mCorrectAnswerBool = subtractResult < sumResult;
                problemText.SetText(num1.ToString() + " - " + num2.ToString() + " " + mStrictInequality + " " + num3.ToString() + " + " + num4.ToString());
            }
        }

        leftButtonText.SetText("O");
        rightButtonText.SetText("X");
    }
    private void GenerateStageNine()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);

        mCorrectAnswerInt = num1 * num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(1, 82); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " * " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageTen()
    {
        int num1 = Random.Range(100, 1000);
        int num2 = Random.Range(100, 1000);

        mCorrectAnswerInt = num1 + num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(200, 2000); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageEleven()
    {
        int num1 = Random.Range(100, 1000);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(100, 1000);
            if (num1 % 10 >= num2 % 10 && (num1 / 10) % 10 >= (num2 / 10) % 10 && num1 > num2) break; // 받아내림 제거
        }

        mCorrectAnswerInt = num1 - num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(100, 1000); // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageTwelve()
    {
        int num1 = Random.Range(10, 100);
        int num2 = Random.Range(1, 10);

        mCorrectAnswerInt = num1 * num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(1, 100) * 10 + mCorrectAnswerInt % 10; // 잘못된 정답 만들기 (정답과 일의자리 수 일치)
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " * " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageThirteen()
    {
        int num1 = Random.Range(1000, 5000);
        int num2 = Random.Range(1000, 5000);

        mCorrectAnswerInt = num1 + num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(100, 1000) * 10 + mCorrectAnswerInt % 10; // 잘못된 정답 만들기 (정답과 일의자리 수 일치)
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " + " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
    private void GenerateStageFourteen()
    {
        int num1 = Random.Range(1000, 10000);
        int num2 = num1;
        while (true)
        {
            num2 = Random.Range(1000, 10000);
            if (num1 % 10 >= num2 % 10 && (num1 / 10) % 10 >= (num2 / 10) % 10 && (num1 / 100) % 10 >= (num2 / 100) % 10 && num1 > num2) break; // 받아내림 제거
        }

        mCorrectAnswerInt = num1 - num2;
        int wrongAnswer = mCorrectAnswerInt;

        while (mCorrectAnswerInt == wrongAnswer) // 중복 방지
        {
            wrongAnswer = Random.Range(0, 900) * 10 + mCorrectAnswerInt % 10; // 잘못된 정답 만들기
        }

        mIsRightSign = Random.Range(0, 2) == 0; // 정답의 방향을 결정 (참이면 오른쪽)

        // 문제를 출력
        problemText.SetText(num1.ToString() + " - " + num2.ToString() + " =");

        if (mIsRightSign) // 정답이 오른쪽
        {
            mCorrectAnswerBool = true;
            leftButtonText.SetText(mCorrectAnswerInt.ToString());
            rightButtonText.SetText(wrongAnswer.ToString());
        }
        else             // 정답이 왼쪽
        {
            mCorrectAnswerBool = false;
            leftButtonText.SetText(wrongAnswer.ToString());
            rightButtonText.SetText(mCorrectAnswerInt.ToString());
        }
    }
}
