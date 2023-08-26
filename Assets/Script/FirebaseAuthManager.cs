using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
public class FirebaseAuthManager
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    private static FirebaseAuthManager instance = null;
    private GameManager mGameManager;
    public Action<bool> LoginState;
    public SceneManager sceneManager;
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
    public void Awake()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
 
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
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
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
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
                Debug.LogError("Canceled Register");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Faulted Register");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("Create User Complete");
        });
    }

    public void Login(string email, string pass)
    {
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Canceled Login");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Faulted Login");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            mGameManager.userName = newUser.UserId.ToString();
            Debug.Log("Login Complete");
            sceneManager.moveMain();
        });
    }

    public void Logout()
    {
        auth.SignOut();
    }
}
