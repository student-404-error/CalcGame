using System;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
public class GameManager : MonoBehaviour
{
    private float startTime;
    private float totalTime;
    public int levelNum;
    private bool isAppInForeground = true;
    private FirebaseAuth auth;
    private DateSave dataSave;
    public string userName; 
    private string date;
    
    public void Start()
    {
        
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        DateSave dateSave = GameObject.Find("DataManager").GetComponent<DateSave>();
        DontDestroyOnLoad(gameObject);
        DateTime dateTime = DateTime.Now;
        auth = FirebaseAuth.DefaultInstance;
        // userName setting dateTime setting
        // userName = "gangmin";
        date = dateTime.ToString("yyyy-MMMM-dd");
        userName = auth.CurrentUser.UserId;
        Debug.Log(userName);
 
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
                        totalTime = snapFloat;
                        // 이제 snapFloat를 사용하여 필요한 작업을 수행할 수 있습니다.
                    }
                    else
                    {
                        // 문자열을 float로 변환할 수 없는 경우에 대한 처리
                        Debug.LogWarning("Cannot convert to float: " + snapString);
                    }
                }
            });
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // 앱이 포그라운드로 진입할 때 시간 측정 시작
            if (!isAppInForeground)
            {
                startTime = Time.realtimeSinceStartup;
                isAppInForeground = true;
            }
        }
        else
        {
            // 앱이 백그라운드로 진입할 때 시간 측정 중단
            if (isAppInForeground)
            {
                totalTime += Time.realtimeSinceStartup - startTime;
                isAppInForeground = false;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // 앱 종료 시 총 시간 저장
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        totalTime += Time.realtimeSinceStartup - startTime;
        Debug.Log(totalTime);
        reference
            .Child(userName)
            .Child(date)
            .Child("TotalTime")
            .SetValueAsync(totalTime);
        // totalTime이 총시간이니깐 데이터베이스에 날짜별로 누적하면 될듯하네요
    }
}
