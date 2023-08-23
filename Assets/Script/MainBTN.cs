using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MainBTN : MonoBehaviour
{
     private GameManager mGameManager;
    // Start is called before the first frame update
    void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnButtonClick(Button clickedButton)
    {
        string buttonText = clickedButton.GetComponentInChildren<TMP_Text>().name;
        mGameManager.levelNum = int.Parse(buttonText);
        Debug.Log(mGameManager.levelNum);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
