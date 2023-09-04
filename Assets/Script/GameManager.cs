using System;
using System.Threading.Tasks;
using Firebase;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;

public class GameManager : MonoBehaviour
{
    private int mAttend;
    private float mStartTime;
    private float mTotalTime;
    public int levelNum;
    private bool mIsAppInForeground = true;
    private FirebaseAuth mAuth;
    public string userName;
    private string mDate;
    private DatabaseReference mReference;
    // playtime;
    public float showTime;

    public async void Start()
    {
        DontDestroyOnLoad(gameObject);

        DateTime dateTime = DateTime.Now;

        mDate = dateTime.ToString("yyyy-MMMM-dd");
        FirebaseApp app = FirebaseApp.DefaultInstance;
        mReference = FirebaseDatabase.GetInstance(app, "https://calcgame-fffc9-default-rtdb.firebaseio.com")
            .RootReference;
        mAuth = FirebaseAuth.DefaultInstance;
        if (mAuth.CurrentUser == null) return;

        userName = mAuth.CurrentUser.UserId;
        await mReference
            .Child(userName)
            .Child(mDate)
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
                        mTotalTime = snapFloat;

                        // 이제 snapFloat를 사용하여 필요한 작업을 수행할 수 있습니다.
                    }
                    else
                    {
                        // 문자열을 float로 변환할 수 없는 경우에 대한 처리
                        Debug.LogWarning("Cannot convert to float: " + snapString);
                    }
                }
            });
        await UpdateAttendanceAsync();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // 앱이 포그라운드로 진입할 때 시간 측정 시작
            if (mIsAppInForeground) return;
            mStartTime = Time.realtimeSinceStartup;
            mIsAppInForeground = true;
        }
        else
        {
            // 앱이 백그라운드로 진입할 때 시간 측정 중단
            if (!mIsAppInForeground) return;
            mTotalTime += Time.realtimeSinceStartup - mStartTime;
            mIsAppInForeground = false;
        }
    }

    private async void OnApplicationQuit()
    {
        // 앱 종료 시 총 시간 저장
        mTotalTime += Time.realtimeSinceStartup - mStartTime;

        // TotalTime 업데이트
        await UpdateTotalTimeAsync();
    }

    private async Task UpdateAttendanceAsync()
    {
        try
        {
            await mReference
                .Child(userName)
                .Child("Attend")
                .Child(mDate)
                .SetValueAsync(true);
            Debug.Log("출석 데이터가 업데이트되었습니다.");
        }
        catch (Exception e)
        {
            // 오류 처리
            Debug.LogError("Firebase 데이터베이스 출석 데이터 업데이트 오류: " + e.Message);
        }
    }

    private async Task UpdateTotalTimeAsync()
    {
        try
        {
            await mReference
                .Child(userName)
                .Child(mDate)
                .Child("TotalTime")
                .SetValueAsync(mTotalTime);
            Debug.Log("TotalTime이 업데이트되었습니다.");
        }
        catch (Exception e)
        {
            // 오류 처리
            Debug.LogError("Firebase 데이터베이스 TotalTime 업데이트 오류: " + e.Message);
        }
    }
}
