using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Scene1BTN : MonoBehaviour
{
    private GameManager mGameManager;
    private DatabaseReference mReference;
    public LevelManager levelManager;
    public int levelNum;
    public int playRound;
    public float playTime;
    public float accuracy;
    public float solvedSpeed;
    public string userName;
    public int score;
    private DateTime mDateTime;
    private string mDate;
    private string mStage;

    private int mCurPlayRound;
    private float mCurAccuracy;
    private float mCurSolvedSpeed;
    private float mCurPlayTime;
    private int mCurScore;

    private async void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            mReference = FirebaseDatabase.GetInstance(app, "https://calcgame-fffc9-default-rtdb.firebaseio.com").RootReference;
        });
    }

    public async void Start()
    {
        mDateTime = DateTime.Now;
        levelNum = levelManager.levelNum;
        userName = mGameManager.userName;
        mDate = mDateTime.ToString("yyyy-MMMM-dd");

        mStage = "S" + levelNum.ToString("D2");
        Debug.Log(mStage);
        // 변수 초기화
        playRound = 0;
        playTime = 0f;
        score = 0;
        await DoAsyncWork(500);
        playRound =  (int)await LoadGameDataAsync("PlayedRound");
        playTime = await LoadGameDataAsync("PlayTime");
        score = (int)await LoadGameDataAsync("Score");

    }

    private async Task DoAsyncWork(int sec)
    {
        await Task.Delay(sec);
        Debug.Log("Async work completed.");
    }

    private async Task<float> LoadGameDataAsync(string dataKey)
    {
        float targetValue = 0f; // 기본값 설정

        try
        {
            DataSnapshot snapshot = await mReference
                .Child(userName)
                .Child(mDate)
                .Child("Problem")
                .Child(mStage)
                .Child(dataKey)
                .GetValueAsync();

            if (snapshot.Exists)
            {
                targetValue = Convert.ToSingle(snapshot.Value);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load {dataKey}: {e.Message}");
        }

        return targetValue;
    }

    public void GoMainFromGame()
    {
        mCurPlayRound = levelManager.playedRound;
        if (mCurPlayRound == 0)
        {
            Debug.Log("you don't play game!!");
        }
        else
        {
            UpdateGameDataAsync("PlayedRound", playRound + mCurPlayRound);

            mCurScore = levelManager.score;
            UpdateGameDataAsync("Score", score + mCurScore);

            mCurAccuracy = levelManager.accuracy;
            float updateAccuracy = (float)(score + mCurScore) / (playRound + mCurPlayRound) * 100;
            mCurSolvedSpeed = levelManager.solvedSpeed;
            float updateSpeed = (playRound + mCurPlayRound) / (playTime + mCurPlayTime) * 60;

            mReference.Child(userName).Child(mDate).Child("Problem").Child(mStage).Child("AverageSpeed")
                .SetValueAsync(updateSpeed);
            mReference.Child(userName).Child(mDate).Child("Problem").Child(mStage).Child("Accuracy")
                .SetValueAsync(updateAccuracy);
        }
        mCurPlayTime = levelManager.playTime;
        UpdateGameDataAsync("PlayTime", playTime + mCurPlayTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void UpdateGameDataAsync(string dataKey, float newValue)
    {
        try
        {
            mReference
                .Child(userName)
                .Child(mDate)
                .Child("Problem")
                .Child(mStage)
                .Child(dataKey)
                .SetValueAsync(newValue);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to update {dataKey}: {e.Message}");
        }
    }
}
