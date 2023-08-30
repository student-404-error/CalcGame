using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Google.MiniJSON;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class DateSave : MonoBehaviour
{
    private string userName;
    private string date;
    private DatabaseReference reference;
    private LoginSystem loginSystem;
    private GameManager mGameManager;

    void Start()
    {
        DateTime dateTime = DateTime.Now;
        loginSystem = GameObject.Find("Canvas").GetComponent<LoginSystem>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        userName = mGameManager.userName;
        date = dateTime.ToString("yyyy-MMMM-dd");


        if (userName != null)
        {
            reference
                .Child(userName)
                .Child(date)
                .Child("Problem")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.Result.Value == null)
                    {
                        InitData();
                    }
                    else if (task.IsCompleted)
                    {
                        Debug.Log(task.Result);
                    }
                });
        }
    }

    public void SetName(string name)
    {
        reference
            .Child(userName)
            .Child("Name")
            .SetValueAsync(name);
    }

    private void InitData()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError("Firebase initialization failed: " + task.Exception);
            }
            else
            {
                reference = FirebaseDatabase.DefaultInstance.RootReference;

                // 데이터 생성
                DateData dateData = new DateData
                {
                    Problem = new ProblemData
                    {
                        S01 = new StageData(),
                        S02 = new StageData(),
                        S03 = new StageData(),
                        S04 = new StageData(),
                        S05 = new StageData(),
                        S06 = new StageData(),
                        S07 = new StageData(),
                        S08 = new StageData(),
                        S09 = new StageData(),
                        S10 = new StageData(),
                        S11= new StageData(),
                        S12 = new StageData(),
                        S13 = new StageData(),
                        S14 = new StageData(),
                    }
                };
                string json = JsonUtility.ToJson(dateData);
                reference.Child(userName).Child(date).SetRawJsonValueAsync(json).ContinueWithOnMainThread(saveTask =>
                {
                    if (saveTask.Exception != null)
                    {
                        Debug.LogError("Failed to save data: " + saveTask.Exception);
                    }
                    else
                    {
                        Debug.Log("Data saved successfully!");
                    }
                });
            }
        });
    }
}
