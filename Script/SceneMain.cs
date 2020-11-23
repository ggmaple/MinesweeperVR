using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace jp.yzroid.CsgUnitySweeper
{
    public class SceneMain : MonoBehaviour
    {

        private GameController mGame;

        [SerializeField]
        private BlockManager mBlock;
        [SerializeField]
        private UiManager mUi;
        [SerializeField]
        private GameObject _GameClear;
        [SerializeField]
        private GameObject _GameOver;
        [SerializeField]
        private GameObject Player;
        [SerializeField]
        private Text _textCountdown;
        [SerializeField]
        private GameObject _PressAStart;
        [SerializeField]
        private GameObject _Gems;
        [SerializeField]
        private OvrAvatar _Avatar;
        [SerializeField]
        private RightController _Right;
        [SerializeField]
        private LeftController _Left;


        private bool ShowController;

        public AudioClip[] clips = new AudioClip[2];
        AudioSource[] sounds;

        public bool clearFlag;
        private bool PressA = true;


        void Awake()
        {
            // ゲーム全体の初期化
            mGame = GameController.Instance;
            mGame.Init();

            // 初期レベルはイージー
            //mGame.GameLevel = GameController.LEVEL_EASY;

            // タイマーの生成
            mTimer = new GameTimer();

            //Componentを取得
            sounds = GetComponents<AudioSource>();

            //ShowController = _Avatar.StartWithControllers;
        }

        //-------------
        // 状態と更新 //
        //---------------------------------------------------------------------------------

        private enum STATE
        {
            LOADING = 0,
            WAIT_START,
            PLAY,
            RESULT
            
        }
        private STATE mState = STATE.LOADING;

        void Update()
        {
            switch (mState)
            {
                // ステージ生成
                case STATE.LOADING:
                    LoadStage();
                    break;
                // スタートボタンが押されるまで何もしないで待機（取り外してOK）
                case STATE.WAIT_START:
                    Player.GetComponent<OVRPlayerController>().enabled = false;
                    if (OVRInput.GetDown(OVRInput.RawButton.A) && PressA)
                    {
                        PressA = false;
                        StartGame();
                    }
                    break;
                // プレイ中：ゲームの終了条件を監視し、終了でない場合はプレイヤーの入力を受け付ける
                case STATE.PLAY:
                    
                    Player.GetComponent<OVRPlayerController>().enabled = true;
                    if (mBlock.IsGameClear)
                    {
                        EndGame(true);
                        return;
                    }
                    if (mBlock.IsGameOver)
                    {
                        EndGame(false);    
                        return;
                    }
                    mBlock.CheckMouseInput();

                    break;
                // 結果表示中:
                case STATE.RESULT:
                    break;
            }
        }

        //---------------------
        // ゲームの開始と終了 //
        //---------------------------------------------------------------------------------

        private void StartGame()
        {
            //ShowController = false;
            mUi.RenewStartText("RESET", "red");
            OnCountdown();
            //StartCoroutine("RenewTime");
            //mState = STATE.PLAY;
        }

        private void ResetGame()
        {
            StopAllCoroutines();
            mTimer.ResetTime();
            mUi.RenewTimeText(mTimer.GetTime());
            mUi.RenewStartText("LOADING", "black");
            mUi.HideResultText();
            mState = STATE.LOADING;
            _GameClear.SetActive(false);
            _GameOver.SetActive(false);
            _PressAStart.SetActive(true);
        }

        private void EndGame(bool clearFlg)
        {
            sounds[0].Stop();
            StopAllCoroutines();
            _Right.OffRight();
            _Left.OffLeft();
            if (clearFlg)
            {
                clearFlag = true;
                sounds[1].PlayOneShot(clips[0]);
                //mUi.ShowResultText("GAME CLEAR!");
                _GameClear.SetActive(true);
                _Gems.SetActive(true);
                ShowController = _Avatar.StartWithControllers;
                ShowController = true;
                _Avatar.ShowLeftController(ShowController);
                _Avatar.ShowRightController(ShowController);
            }
            else
            {
                clearFlag = false;
                //mUi.ShowResultText("GAME OVER");
                _GameOver.SetActive(true);
                ShowController = _Avatar.StartWithControllers;
                ShowController = true;
                _Avatar.ShowLeftController(ShowController);
                _Avatar.ShowRightController(ShowController);
            }
            mState = STATE.RESULT;
        }

        //-----------------
        // ステージの生成 //
        //---------------------------------------------------------------------------------

        private void LoadStage()
        {
            int gameLevel = mGame.GameLevel;
            mBlock.CreateField(gameLevel);
           // mCamera.SetLimit(gameLevel);
            mUi.RenewStartText("START", "blue");
            mState = STATE.WAIT_START;
        }

        //------------
        // 時間管理 //
        //---------------------------------------------------------------------------------

        public GameTimer mTimer;

        private IEnumerator RenewTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                mTimer.IncTime();
                mUi.RenewTimeText(mTimer.GetTime());
            }
        }

        //-----------
        // 入力待機 //
        //---------------------------------------------------------------------------------

        public void OnStart()
        {
            switch (mState)
            {
                case STATE.WAIT_START:
                    StartGame();
                    break;
                case STATE.PLAY:
                case STATE.RESULT:
                    ResetGame();
                    break;
            }
        }

        public void OnSelectLevel(Dropdown dropdown)
        {
            switch (dropdown.value)
            {
                case 0:
                    mGame.GameLevel = GameController.LEVEL_EASY;
                    break;
                case 1:
                    mGame.GameLevel = GameController.LEVEL_NORMAL;
                    break;
                case 2:
                    mGame.GameLevel = GameController.LEVEL_HARD;
                    break;
            }
            ResetGame();
        }

        public void OnCountdown()
        {
            Debug.Log("count");
            _textCountdown.text = "";
            StartCoroutine(CountdownCoroutine());
        }

        IEnumerator CountdownCoroutine()
        {
            _PressAStart.SetActive(false);
            _textCountdown.gameObject.SetActive(true);
            sounds[1].PlayOneShot(clips[1]);

            _textCountdown.text = "3";
            yield return new WaitForSeconds(1.0f);

            _textCountdown.text = "2";
            yield return new WaitForSeconds(1.0f);

            _textCountdown.text = "1";
            yield return new WaitForSeconds(1.0f);

            _textCountdown.text = "GO!";
            yield return new WaitForSeconds(1.0f);

            _textCountdown.text = "";
            _textCountdown.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine("RenewTime");
            sounds[0].Play();
            mState = STATE.PLAY;
            PressA = true;
            _Right.OnRight();
            _Left.OnLeft();
        }

    }
}
