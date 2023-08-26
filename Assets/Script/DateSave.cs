using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Extensions;
using UnityEditor.SceneManagement;

public class DateSave : MonoBehaviour
{
    // private DatabaseReference databaseReference;
    // Start is called before the first frame update
    private LoginSystem loginSystem;
    private string userName;
    private string date;
    // public DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    private GameManager mGameManager;
    void Start()
    {
        DateTime dateTime = DateTime.Now;
        loginSystem = GameObject.Find("Canvas").GetComponent<LoginSystem>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // userName = loginSystem.name.text;
        userName = mGameManager.userName;
        Debug.Log(userName);
        date = dateTime.ToString("yyyy-MMMM-dd");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetDataToDatabase("users");
    }

    // Update is called once per frame
    void Update()
    {
        
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // reference
        //     .Child(userName)
        //     .Child(date)
        //     .Child("PlayTime")
        //     .SetValueAsync("15h 30m 20s");
        reference
            .Child(userName)
            .Child(date)
            .Child("Problem1")
            .Child("S1")
            .Child("Avg_time")
            .SetValueAsync("30m 23s");
        reference
            .Child(userName)
            .Child(date)
            .Child("Problem")
            .Child("S1")
            .Child("SolvedNum")
            .SetValueAsync("15");
        
        
// 데이터 읽기
        reference
            .Child("users")
            .Child("user_id")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string data = snapshot.Value.ToString();
                }
            });
    }

    public string GetDataToDatabase(string obj, string level = "S1")
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        string problemInfo = "";
        string snap = "";
        switch (obj)
        {
            case "users":
                reference
                    .Child(userName)
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Name");
                        }
                        else if (task.IsCompleted)
                        {
                            snap = task.Result.Key;
                        }
                    });
                break;
            case "problem":
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Problem")
                    .Child(level)
                    .Child("AvgSolvedTime")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            problemInfo += task.Result.Key+",";
                        }
                    });
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Problem")
                    .Child(level)
                    .Child("AllProblemCount")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            problemInfo += task.Result.Key+",";
                        }
                    }); 
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Problem")
                    .Child(level)
                    .Child("CorrectAnswerCount")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            problemInfo += task.Result.Key+",";
                        }
                    });
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Problem")
                    .Child(level)
                    .Child("LvPlayTime")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            problemInfo += task.Result.Key+",";
                        }
                    });
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Problem")
                    .Child(level)
                    .Child("CorrectRatio")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            problemInfo += task.Result.Key +",";
                        }
                    });
                return problemInfo;
            case "attend":
                reference
                    .Child(userName)
                    .Child(date)
                    .Child("Attend")
                    .GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Not available Level");
                        }
                        else if (task.IsCompleted)
                        {
                            snap = task.Result.Key;
                        }
                    });
                break;
        }
        return snap;
    }

    public float GetTotalTime()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        float result = 0.0f;
        reference
            .Child(userName)
            .Child(date)
            .Child("TotalTime")
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
                    Debug.Log(snapString);
                    if (float.TryParse(snapString, out float snapFloat))
                    {
                        // 문자열을 float로 성공적으로 변환한 경우, snapFloat에 할당됩니다.
                        Debug.Log("Converted to float: " + snapFloat);
                        result = snapFloat;
                        // 이제 snapFloat를 사용하여 필요한 작업을 수행할 수 있습니다.
                    }
                    else
                    {
                        // 문자열을 float로 변환할 수 없는 경우에 대한 처리
                        Debug.LogWarning("Cannot convert to float: " + snapString);
                    }
                }
            });
        return result;
    }

    public void TotalTimeUpdate(string totalTime)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference; 
        reference
            .Child(userName)
            .Child(date)
            .Child("TotalTime")
            .SetValueAsync(totalTime);
    }

    public void DataInit()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // string lv;
        // for (int i = 1; i < 15; i++)
        // {
        //     lv = $"S{i}";
        //     reference
        //         .Child(userName)
        //         .Child(date)
        //         .Child("Problem")
        //         .Child(lv)
        //         .Child("AvgSolvedTime")
        //         .SetValueAsync("0");
        //     reference
        //         .Child(userName)
        //         .Child(date)
        //         .Child("Problem")
        //         .Child(lv)
        //         .Child("CorrectAnswerCount")
        //         .SetValueAsync("0");
        //     reference
        //         .Child(userName)
        //         .Child(date)
        //         .Child("Problem")
        //         .Child(lv)
        //         .Child("AllProblemCount")
        //         .SetValueAsync("0");
        //     reference
        //         .Child(userName)
        //         .Child(date)
        //         .Child("Problem")
        //         .Child(lv)
        //         .Child("LvPlayTime")
        //         .SetValueAsync("0");
        //     reference
        //         .Child(userName).Child(date)
        //         .Child("Problem")
        //         .Child("S" + i)
        //         .Child("CorrectRatio")
        //         .SetValueAsync("0");
        // }
        reference
            .Child(userName)
            .Child(date)
            .Child("TotalTime")
            .SetValueAsync("0");
        reference
            .Child(userName)
            .Child(date)
            .Child("Attend")
            .SetValueAsync("0");
    }

    public void SetName(string name)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference
            .Child(userName)
            .Child("Name")
            .SetValueAsync(name);
    }

    public void GetName()
    {
        
    }
}
