using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
public class DateSave : MonoBehaviour
{
    // private DatabaseReference databaseReference;
    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
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
}
