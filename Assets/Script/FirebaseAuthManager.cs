using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using TMPro;
using System;
using System.Threading.Tasks;

public class FirebaseAuthManager : MonoBehaviour
{
    public GameObject Popup; // 로그인 실패시 팝업창
    public TMP_Text ErrorMessage; // 에러 메세지
    
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
            mAuth = FirebaseAuth.DefaultInstance;
        });

        // 나머지 코드는 그대로 두세요.
    }
    private async Task DoAsyncWork()
    {
        // 비동기 작업 수행
        await Task.Delay(200); // 예시로 0.2초 대기
        Debug.Log("Async work completed.");
    }
    public async void Awake()
    {
        mSceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        mDataSave = GameObject.Find("DataManager").GetComponent<DateSave>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mEm = email.text;
        mPa = pass.text;
        
        await DoAsyncWork();
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
    public async void Create()
    {
        string email = this.email.text;
        string password = pass.text;

        try
        {
            var registrationTask = mAuth.CreateUserWithEmailAndPasswordAsync(email, password);

            await registrationTask;

            if (registrationTask.IsCompleted)
            {
                FirebaseUser newUser = registrationTask.Result.User;
                Debug.Log($"User {newUser.Email} registered successfully!");
                Popup.SetActive(true);
                ErrorMessage.SetText($"User {newUser.Email} registered successfully!");
            }
            else
            {
                Popup.SetActive(true);
                Debug.LogError("Registration failed.");
            }
        }
        catch (Exception ex)
        {
            Popup.SetActive(true);
            Debug.Log($"Error during registration: {ex.Message}");
            ErrorMessage.SetText(ex.Message);
        }
    }
    public async void Login()
    {
        string email = this.email.text;
        string password = pass.text;

        try
        {
            var signInTask = mAuth.SignInWithEmailAndPasswordAsync(email, password);

            await signInTask;

            if (signInTask.IsCompleted)
            {
                FirebaseUser newUser = signInTask.Result.User;
                Debug.Log($"User {newUser.Email} SignIn successfully!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); // 로그인 성공 시에만 씬 이동
            }
            else
            {
                Popup.SetActive(true);
                Debug.LogError("SignIn failed.");
            }
        }
        catch (Exception ex)
        {
            Popup.SetActive(true);
            Debug.Log($"Error during SignIn: {ex.Message}");
            ErrorMessage.SetText(ex.Message);
        }
    }


// 메인 씬 이동을 메인 스레드에서 처리하는 메서드
    public void LoadMainSceneOnMainThread()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    public void Logout()
    {
        mAuth.SignOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }
}
