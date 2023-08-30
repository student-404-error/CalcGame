using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Scene1BTN : MonoBehaviour
{
    private GameManager mGameManager;
    private DatabaseReference reference;
    public LevelManager levelManager;
    public int levelNum;    //stage data
    public int playRound;
    public float playTime;
    public float accuracy;
    public float solvedSpeed;
    public string userName;
    public int score;
    public DateTime dateTime;
    private string date;
    private string stage;
    
    private int curPlayRound;
    private float curPlayTime;
    private float curAccuracy;
    private int curScore;
    private float curSolvedSpeed;
    public void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        dateTime = DateTime.Now;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelNum = levelManager.levelNum;       // lv
        
        userName = mGameManager.userName;       // userName
        date = dateTime.ToString("yyyy-MMMM-dd");
        stage = "S";
        if (levelNum < 10)
        {
            stage += "0" + levelNum.ToString("D");
        }
        else
        {
            stage += levelNum.ToString();
        }
        reference
            .Child(userName)
            .Child(date)
            .Child("Problem")
            .Child(stage)
            .Child("PlayedRound")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Faulted Road PlayedRound");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string snapString = snapshot.Value.ToString();
                    playRound = int.Parse(snapString);
                    Debug.Log(playRound);
                }
            });
        reference
            .Child(userName)
            .Child(date)
            .Child("Problem")
            .Child(stage)
            .Child("PlayTime")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Not available Time");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result; // GetValueAsync의 결과값을 가져옴
                    string snapString = snapshot.Value.ToString();
                    if (float.TryParse(snapString, out float snapFloat))
                    {
                        // 문자열을 float로 성공적으로 변환한 경우, snapFloat에 할당됩니다.
                        Debug.Log("Converted to float: " + snapFloat);
                        playTime = snapFloat;
                        // 이제 snapFloat를 사용하여 필요한 작업을 수행할 수 있습니다.
                    }
                    else
                    {
                        // 문자열을 float로 변환할 수 없는 경우에 대한 처리
                        Debug.LogWarning("Cannot convert to float: " + snapString);
                    }
                }
            });
        reference
            .Child(userName)
            .Child(date)
            .Child("Problem")
            .Child(stage)
            .Child("Score")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Faulted Road PlayedRound");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string snapString = snapshot.Value.ToString();
                    score = int.Parse(snapString);
                    Debug.Log(score);
                }
            });
        
    }

    public void GoMainFromGame()
    {
        
        curPlayTime = levelManager.playTime;       // play time
        reference.Child(userName).Child(date).Child("Problem").Child(stage).Child("PlayTime")
            .SetValueAsync(playTime + curPlayTime);
        
        curPlayRound = levelManager.playedRound;   // question Count
        reference.Child(userName).Child(date).Child("Problem").Child(stage).Child("PlayedRound")
            .SetValueAsync(playRound + curPlayRound);
        
        curScore = levelManager.score; 
        reference.Child(userName).Child(date).Child("Problem").Child(stage).Child("Score")
            .SetValueAsync(score + curScore);

        curAccuracy = levelManager.accuracy;       // accuracy
        float updateAccuracy = (float)(score + curScore) / (playRound + curPlayRound) * 100;

        curSolvedSpeed = levelManager.solvedSpeed; // Avg Solve Speed
        float updateSpeed = (playRound + curPlayRound) / (playTime + curPlayTime) * 60;

        reference.Child(userName).Child(date).Child("Problem").Child(stage).Child("AverageSpeed")
            .SetValueAsync(updateSpeed);
        reference.Child(userName).Child(date).Child("Problem").Child(stage).Child("Accuracy")
            .SetValueAsync(updateAccuracy);// data send
        // move scene to main
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
