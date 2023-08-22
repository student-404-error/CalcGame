using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    // public Button moveStageOne;
    // public Button moveStageTwo;
    // public Button moveStageThree;
    // public Button moveStageFour;
    // public Button moveStageFive;
    // public Button moveStageSix;
    // public Button moveStageSeven;

    public void moveScene(string sceneName)
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("not exist SceneName you input");
        }
        
    }
}
