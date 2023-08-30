using System;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
public class GameManager : MonoBehaviour
{
    private int attend;
    private float startTime;
    private float totalTime;
    public int levelNum;
    private bool isAppInForeground = true;
    private FirebaseAuth auth;
    private DateSave dataSave;
    public string userName;
    private string date;
    private string yesterday;
    private DatabaseReference reference;
    private bool isNewDay = false;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);

        DateSave dateSave = GameObject.Find("DataManager").GetComponent<DateSave>();
        DateTime dateTime = DateTime.Now;
        
        date = dateTime.ToString("yyyy-MMMM-dd");
        yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MMMM-dd");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        
        
        if (auth.CurrentUser != null)
        {
            userName = auth.CurrentUser.UserId;
            reference
                .Child(userName)
                .Child(date)
                .Child("TotalTime")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.Result.Value == null)
                    {
                        isNewDay = true;
                        Debug.Log("New Day");
                    }
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
            reference
                .Child(userName)
                .Child("Attend")
                .Child("continued")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.Exception != null)
                    {
                        Debug.LogError("Invalid attends");
                    }
                    else if (task.Result.Value != null)
                    {
                        DataSnapshot snapshot = task.Result;
                        string snapString = snapshot.Value.ToString();
                        if (int.TryParse(snapString, out int snapInt))
                        {
                            // 문자열을 int로 성공적으로 변환한 경우, snapInt에 할당됩니다.
                            Debug.Log("Converted to int: " + snapInt);
                            attend = snapInt;
                            // 이제 snapInt 사용하여 필요한 작업을 수행할 수 있습니다.
                        }
                        else
                        {
                            // 문자열을 int로 변환할 수 없는 경우에 대한 처리
                            Debug.LogWarning("Cannot convert to int: " + snapString);
                        }
                    }
                });
        }
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
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        totalTime += Time.realtimeSinceStartup - startTime;

        // 출석 체크 데이터 초기화
        if (isNewDay)
        {
            reference
                .Child(userName)
                .Child("Attend")
                .Child("check")
                .SetValueAsync(false);
        }

        
        CheckAttendance();
        CheckYesterday();
        reference
            .Child(userName)
            .Child(date)
            .Child("TotalTime")
            .SetValueAsync(totalTime);
        
    }
    private void CheckAttendance()
    {
        // 출석체크 데이터 확인
        reference
            .Child(userName)
            .Child("Attend")
            .Child("check")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError("출석체크 데이터 확인 실패: " + task.Exception);
                    return;
                }

                // 출석 여부 체크
                if (Convert.ToBoolean(task.Result.Value) == false)
                {
                    Debug.Log("출석되지 않았습니다. 출석 처리 중...");
                    MarkAttendance();
                }
                else
                {
                    Debug.Log("이미 출석되었습니다.");
                }
            });
    }

    private void MarkAttendance()
    {
        // 출석체크 데이터 기록
        reference
            .Child(userName)
            .Child("Attend")
            .Child("check")
            .SetValueAsync(true)
            .ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError("출석체크 기록 실패: " + task.Exception);
                    return;
                }

                attend += 1;
                Debug.Log("출석체크가 정상적으로 기록되었습니다.");
                Debug.Log(attend);
                reference
                    .Child(userName)
                    .Child("Attend")
                    .Child("continued")
                    .SetValueAsync(attend);
            });
    }

    private void CheckYesterday()
    {
        reference
            .Child(userName)
            .Child(yesterday)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.Result.Value == null)
                {
                    attend = 1; // 출석 일수 초기화
                }
                // 출석 일수 업데이트
                reference
                    .Child(userName)
                    .Child("Attend")
                    .Child("continued")
                    .SetValueAsync(attend);
            });
    }
    
}






    // private void AttendCheck()
    // {
    //     reference
    //         .Child(userName)
    //         .Child(date)
    //         .Child("AttendCheck")
    //         .Child("TodayConnect")
    //         .GetValueAsync()
    //         .ContinueWithOnMainThread(task =>
    //         {
    //             DataSnapshot snapshot = task.Result;
    //             bool checkToday = snapshot.Value;
    //             if (checkToday)
    //             {
    //                 
    //             }
    //         });
    // }