using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using TMPro;
using System;
using System.Threading.Tasks;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth mAuth;
    private FirebaseUser mUser;
    private static FirebaseAuthManager _instance = null;
    private GameManager mGameManager;
    public TMP_InputField email;
    public TMP_InputField pass;
    public Action<bool> loginState;
    private DateSave mDataSave;
    private DatabaseReference mReference;
    private SceneManager mSceneManager;
    private string mEm, mPa;
    public string UserId => mUser.UserId;
    public static FirebaseAuthManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FirebaseAuthManager>();
            }

            return _instance;
        }
    }
    public void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            mReference = FirebaseDatabase.GetInstance(app, "https://calcgame-fffc9-default-rtdb.firebaseio.com").RootReference;

        });

        // 나머지 코드는 그대로 두세요.
    }
    private async Task DoAsyncWork()
    {
        // 비동기 작업 수행
        await Task.Delay(500); // 예시로 1초 대기
        Debug.Log("Async work completed.");
    }
    public async void Awake()
    {
        await DoAsyncWork();
        mSceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        mDataSave = GameObject.Find("DataManager").GetComponent<DateSave>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mEm = email.text;
        mPa = pass.text;
        
        mAuth = FirebaseAuth.DefaultInstance;
        
        if (mAuth.CurrentUser != null)
        {
            // 이미 로그인된 유저가 있다면, 다음 화면으로 이동 등의 처리를 해줄 수 있습니다.
            mDataSave.InitializeFirebaseData();
            mSceneManager.MoveMain();
        }
        else
        {
            // auth.StateChanged += OnChanged;
            Init();
        } 
    }
    public void Init()
    {
        mAuth = FirebaseAuth.DefaultInstance;

        if (mAuth.CurrentUser != null)
        {
            Logout();
        }

        mAuth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if (mAuth.CurrentUser != null)
        {
            bool signed = (mAuth.CurrentUser != mUser && mAuth.CurrentUser != null);
            if (!signed && mUser != null)
            {
                loginState?.Invoke(false);
            }

            mUser = mAuth.CurrentUser;
            if (signed)
            {
                loginState?.Invoke(true);
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public void Create()
    {
        mEm = email.text;
        mPa = pass.text;
        mAuth.CreateUserWithEmailAndPasswordAsync(mEm, mPa).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Canceled Register");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Faulted Register");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("Create User Complete");
            Login();
        });
    }

    public async void Login()
    {
        mEm = email.text;
        mPa = pass.text;
    
        try
        {
            var loginTask = mAuth.SignInWithEmailAndPasswordAsync(mEm, mPa);
            await loginTask; // 이 부분에서 로그인 작업이 완료될 때까지 기다립니다.
        
            if (loginTask.IsCanceled)
            {
                Debug.LogError("Canceled Login");
                return;
            }
        
            if (loginTask.IsFaulted)
            {
                foreach (Exception exception in loginTask.Exception.Flatten().InnerExceptions)
                {
                    string errorCode = exception.GetType().Name;
                    Debug.LogError($"Faulted Login with error code: {errorCode}");
                }
                return;
            }
        
            FirebaseUser newUser = loginTask.Result.User;
        
            if (newUser != null)
            {
                mGameManager.userName = newUser.UserId;
                Debug.Log("Login Complete");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("Login Result is null.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error during login: " + ex.Message);
        }
    }



    public void Logout()
    {
        mAuth.SignOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }
}
