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

    // public void moveScene(string sceneName)
    // {
    //     Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Main");
    //     Debug.Log(scene.IsValid());
    //     if (scene.IsValid())
    //     {
    //         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    //         // UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    //         
    //     }
    //     else
    //     {
    //         Debug.LogError("not exist SceneName you input");
    //     }
    // }

    public void moveMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
