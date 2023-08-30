using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainBTN : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameManager mGameManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnButtonClick(Button clickedButton)
    {
        string buttonText = clickedButton.GetComponentInChildren<TMP_Text>().name;
        mGameManager.levelNum = int.Parse(buttonText);
        sceneManager.moveStage();
        Debug.Log(mGameManager.levelNum);
    }
    
    // Update is called once per frame
}
