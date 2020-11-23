using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1.UIシステムを使うときに必要なライブラリ
using UnityEngine.UI;
// 2.Scene関係の処理を行うときに必要なライブラリ
using UnityEngine.SceneManagement;
using jp.yzroid.CsgUnitySweeper;

public class ButtonManager : MonoBehaviour
{
    private GameController mGame;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム全体の初期化
        mGame = GameController.Instance;
        mGame.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEasy()
    {
        mGame.GameLevel = GameController.LEVEL_EASY;
        SceneManager.LoadScene("main");
    }

    public void OnNormal()
    {
        mGame.GameLevel = GameController.LEVEL_NORMAL;
        SceneManager.LoadScene("main");
    }

    public void OnHard()
    {
        mGame.GameLevel = GameController.LEVEL_HARD;
        SceneManager.LoadScene("main");
    }
}
