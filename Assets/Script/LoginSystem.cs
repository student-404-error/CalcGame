using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoginSystem : MonoBehaviour
{
    public TMP_InputField email;

    public TMP_InputField pass;

    public TMP_Text outputText;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
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
