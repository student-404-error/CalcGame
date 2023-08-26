using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1BTN : MonoBehaviour
{
    public LevelManager levelManager;
    
    public void GoMainFromGame()
    {
        // public int levelNum;
        // public float startTime;
        // public float playTime;
        // public float solvedSpeed;
        
        // data send
        
        // move scene to main
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
