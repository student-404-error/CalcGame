using UnityEngine;
using Firebase.Auth;
using TMPro;

public class Login : MonoBehaviour {

    [SerializeField] string email;
    [SerializeField] string password;

    public TMP_InputField inputTextEmail;
    public TMP_InputField inputTextPassword;
    public TMP_Text loginResult;

    FirebaseAuth auth;

    void Awake()
    {
        // 초기화
        auth = FirebaseAuth.DefaultInstance;
    }

    // 버튼이 눌리면 실행할 함수.
    public void JoinBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        CreateUser();
    }

    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "회원가입 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "회원가입 실패";
                return;
            }

            // AuthResult 객체에서 FirebaseUser 객체로 변환
            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            loginResult.text = "회원가입 굿럭";
        });
    }
}