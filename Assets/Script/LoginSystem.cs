using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using TMPro;
public class LoginSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField pass;
    public TMP_Text outputText;
    private FirebaseAuth auth;
    public SceneManager sceneManager;
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        auth = FirebaseAuth.DefaultInstance;
        
        if (auth.CurrentUser != null)
        {
            // 이미 로그인된 유저가 있다면, 다음 화면으로 이동 등의 처리를 해줄 수 있습니다.
            sceneManager.moveMain();
        }
        else
        {
            FirebaseAuthManager.Instance.LoginState += OnChangedState;
            FirebaseAuthManager.Instance.Init();
        }
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "Login : " : "Logout : ";
        outputText.text += FirebaseAuthManager.Instance.UserId;

    }

    public void Create()
    {
        string e = email.text;
        string p = pass.text;
        
        FirebaseAuthManager.Instance.Create(e, p);
    }

    public void Login()
    {   
        FirebaseAuthManager.Instance.Login(email.text, pass.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.Logout();
    }
}
