using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
public class DateSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        print(reference);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
