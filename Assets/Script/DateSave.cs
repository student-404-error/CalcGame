using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
public class DateSave : MonoBehaviour
{
    // private DatabaseReference databaseReference;
    // Start is called before the first frame update
    private LoginSystem loginSystem;
    private string userName;
    private string date;
    public int a = 1;
    void Start()
    {
        DateTime dateTime = DateTime.Now;
        loginSystem = GameObject.Find("Canvas").GetComponent<LoginSystem>();
        // userName = loginSystem.name.text;
        userName = "gangmin";
        date = dateTime.ToString("yyyy-MMMM-dd");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
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
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string data = snapshot.Value.ToString();
                }
            });
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
    public void WriteDatabase(string userName, string date)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference
            .Child("users")
            .Child("test")
            .SetValueAsync("hihi");
        reference
            .Child("users")
            .Child("user_id")
            .SetValueAsync("hello gangmin");
    }
}
