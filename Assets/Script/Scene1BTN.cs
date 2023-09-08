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

    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        
    }

    public async void Start()
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            mReference = FirebaseDatabase.GetInstance(app, "https://calcgame-fffc9-default-rtdb.firebaseio.com").RootReference;
        });
        mDateTime = DateTime.Now;
        levelNum = levelManager.levelNum;
        userName = mGameManager.userName;
        mDate = mDateTime.ToString("yyyy-MMMM-dd");

        mStage = "S" + levelNum.ToString("D2");
        Debug.Log("now level:" + mStage);
        // 변수 초기화
        playRound = 0;
        playTime = 0f;
        score = 0;
        
        playRound =  (int)await LoadGameDataAsync("PlayedRound");
        playTime = await LoadGameDataAsync("PlayTime");
        score = (int)await LoadGameDataAsync("Score");
        // Debug.Log($"{playRound}, {playTime}, {score}");
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

    public async void GoMainFromGame()
    {
        mCurPlayRound = levelManager.playedRound;
        await DoAsyncWork(300);
        if (mCurPlayRound == 0)
        {
            Debug.Log("you don't play game!!");
        }
        else
        {
            UpdateGameDataAsyncInt("PlayedRound", playRound + mCurPlayRound);

            mCurScore = levelManager.score;
            UpdateGameDataAsyncInt("Score", score + mCurScore);

            mCurAccuracy = levelManager.accuracy;
            float updateAccuracy = (float)(score + mCurScore) / (playRound + mCurPlayRound) * 100;
            mCurSolvedSpeed = levelManager.solvedSpeed;
            float updateSpeed = (playTime + mCurPlayTime) / (playRound + mCurPlayRound);
            Debug.Log($"{playRound}, {mCurPlayRound}, {playTime}, {mCurPlayTime} update=> {updateAccuracy}, {updateSpeed}");

            UpdateGameDataAsyncFloat("AverageSpeed", updateSpeed);
            UpdateGameDataAsyncFloat("Accuracy", updateAccuracy);
        }
        mCurPlayTime = levelManager.playTime;
        UpdateGameDataAsyncFloat("PlayTime", playTime + mCurPlayTime);
        Debug.Log($"{playRound + mCurPlayRound}, {playTime + mCurPlayTime}, {score + mCurScore}, {(playRound + mCurPlayRound) / (playTime + mCurPlayTime) * 60}, {(float)(score + mCurScore) / (playRound + mCurPlayRound) * 100}");

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void UpdateGameDataAsyncInt(string dataKey, int newValue)
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
            Debug.LogError($"Failed to update Int {dataKey}: {e.Message}");
        }
    }
    private void UpdateGameDataAsyncFloat(string dataKey, float newValue)
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
            Debug.LogError($"Failed to update Float {dataKey}: {e.Message}");
        }
    }
}
