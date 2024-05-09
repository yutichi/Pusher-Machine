using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //スクリプト
    public EventManager _eventManager;
    public SEManager _seManager;

    //お金
    [SerializeField]
    public int _credit;

    //お金(テキスト)
    [SerializeField]
    public TextMeshProUGUI _creditText;

    //スコア
    public static int _score;

    //スコア(テキスト)
    [SerializeField]
    public TextMeshProUGUI _scoreText;

    //タイトル関連
    [SerializeField]
    private GameObject _title, _creditActive, _jackPotActive;

    //ゲームモード選択画面
    [SerializeField]
    private GameObject _modeScene;

    //ゲームオーバー関連
    [SerializeField]
    public GameObject _gameOver;
    private float _gameOverTimmer;
    private bool _gameOverTrigger;

    //ゲーム外時のトリガー
    public static bool _stopTrigger;

    //データがあるかの確認
    private int _saveDataCheck;

    //ゲームオーバー、リザルト時のSE用トリガー
    [SerializeField]
    private bool _resultSETrigger;

    // Start is called before the first frame update
    void Start()
    {
        _title.SetActive(true);
        _creditActive.SetActive(false);
        _jackPotActive.SetActive(false);
        _modeScene.SetActive(false);

        _gameOver.SetActive(false);

        _gameOverTrigger = false;
        _gameOverTimmer = 10f;

        _stopTrigger = true;

        _score = 0;

        _saveDataCheck = PlayerPrefs.GetInt("DataCheck");

        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _creditText.text = "CREDIT:" + _credit;

        //お金が無くなったら際の処理
        if (_credit <= 0) 
        {
            _gameOverTrigger = true;

            //10秒待ってお金が無ければ処理
            if(_gameOverTimmer <= 0)
            {
                //ゲームオーバー
                _scoreText.text = "SCORE:" + _score;
                _gameOver.SetActive(true);

                _stopTrigger = true;

                //リザルトSE
                if(_resultSETrigger == true) 
                {
                    //通常モード時
                    SEManager._audioSource.PlayOneShot(_seManager._gameOverSE);
                }
                else if(_resultSETrigger == false)
                {
                    //特殊モード時
                    SEManager._audioSource.PlayOneShot(_seManager._resultSE);
                }

                Time.timeScale = 0f;
            }
        }
        else if (_credit >= 1) 
        {
            _gameOverTrigger = false;
        }

        //お金が無くなったら10秒待つ
        if(_gameOverTrigger == false)
        {
            _gameOverTimmer = 10f;
        }
        else if(_gameOverTrigger == true) 
        {
            _gameOverTimmer -= Time.deltaTime;
        }
    }

    //モード選択画面に移行する
    public void ModeScene()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _title.SetActive(false);
        _modeScene.SetActive(true);

        if(_saveDataCheck > 0)
        {
            //データをロードする
            _credit = PlayerPrefs.GetInt("Credit");
            EventManager._jackPotCredit = PlayerPrefs.GetInt("JackPotCredit");
            _score = PlayerPrefs.GetInt("Score");
        }
    }

    //時間制限モード
    public void TimeMode()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(1);
    }

    //球制限モード
    public void RemainingMode()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(2);
    }

    //ゲームスタートボタン
    public void StartGame()
    {
        _creditActive.SetActive(true);
        _jackPotActive.SetActive(true);
        _title.SetActive(false);
        _modeScene.SetActive(false);
        _stopTrigger = false;

        PauseManager._pauseStop = false;

        Time.timeScale = 1f;

        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);
    }

    //リスポーンボタン
    public void Respawn()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        //スコア、ジャックポットクレジット、球数がリセットされる
        _score = 0;
        _credit = 100;
        EventManager._jackPotCredit = 100;

        //ゲーム再スタート
        _gameOver.SetActive(false);

        _stopTrigger = false;
        Time.timeScale = 1f;
    }

    //タイトルに戻る(サブモード用)
    public void BackTitleButton()
    {
        SceneManager.LoadScene(0);
    }
}
