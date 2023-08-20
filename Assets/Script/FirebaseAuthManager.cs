using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;
public class FirebaseAuthManager
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    private static FirebaseAuthManager instance = null;
    public Action<bool> LoginState;
    public string UserId => user.UserId;
    public static FirebaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            }

            return instance;
        }
    }

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser != null)
        {
            Logout();
        }

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != null)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public void Create(string email, string pass)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("canceled register");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("canceled register");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("Create User complete");
        });
    }

    public void Login(string email, string pass)
    {
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("canceled login");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("canceled login");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("login complete");
        });
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("logout");
    }
}
