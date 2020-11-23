using jp.yzroid.CsgUnitySweeper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBack : MonoBehaviour
{
    [SerializeField]
    private SceneMain _main;
    
    private GameController mGame;

    // Start is called before the first frame update
    void Start()
    {
        mGame = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {        
        if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B))
        {
            //Debug.Log("Xボタンを押した");
            SceneManager.LoadScene("Title");
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.X) || OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (_main.clearFlag == true)
            {
                if (_main.mTimer.GetTime() < 60)
                {
                    switch (mGame.GameLevel)
                    {
                        case GameController.LEVEL_EASY:
                            mGame.GameLevel = GameController.LEVEL_NORMAL;
                            break;
                        case GameController.LEVEL_NORMAL:
                            mGame.GameLevel = GameController.LEVEL_HARD;
                            break;
                        case GameController.LEVEL_HARD:
                            mGame.GameLevel = GameController.LEVEL_HARD;
                            break;
                    }
                }
                else if (_main.mTimer.GetTime() < 240)
                {
                    mGame.GameLevel = mGame.GameLevel;
                }
                else if (_main.mTimer.GetTime() >= 240)
                {
                    switch (mGame.GameLevel)
                    {
                        case GameController.LEVEL_EASY:
                            mGame.GameLevel = GameController.LEVEL_EASY;
                            break;
                        case GameController.LEVEL_NORMAL:
                            mGame.GameLevel = GameController.LEVEL_EASY;
                            break;
                        case GameController.LEVEL_HARD:
                            mGame.GameLevel = GameController.LEVEL_NORMAL;
                            break;
                    }
                }
            }
            else
            {
                switch (mGame.GameLevel)
                {
                    case GameController.LEVEL_EASY:
                        mGame.GameLevel = GameController.LEVEL_EASY;
                        break;
                    case GameController.LEVEL_NORMAL:
                        mGame.GameLevel = GameController.LEVEL_EASY;
                        break;
                    case GameController.LEVEL_HARD:
                        mGame.GameLevel = GameController.LEVEL_NORMAL;
                        break;
                }
            }
            Debug.Log(mGame.GameLevel);

            SceneManager.LoadScene("main");
        }
    }
}
