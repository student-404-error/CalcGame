using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float startTime;
    private float totalTime;
    private DateSave dataSave;
    public int levelNum;
    private bool isAppInForeground = true;
    public void Start()
    {
        
        dataSave = GameObject.Find("DataManager").GetComponent<DateSave>();
        DontDestroyOnLoad(gameObject);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // 앱이 포그라운드로 진입할 때 시간 측정 시작
            if (!isAppInForeground)
            {
                startTime = Time.realtimeSinceStartup;
                isAppInForeground = true;
            }
        }
        else
        {
            // 앱이 백그라운드로 진입할 때 시간 측정 중단
            if (isAppInForeground)
            {
                totalTime += Time.realtimeSinceStartup - startTime;
                isAppInForeground = false;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // 앱 종료 시 총 시간 저장
        totalTime += Time.realtimeSinceStartup - startTime;
        Debug.Log(totalTime);
        dataSave.TotalTimeUpdate(totalTime.ToString("F"));
        // totalTime이 총시간이니깐 데이터베이스에 날짜별로 누적하면 될듯하네요
    }
}
