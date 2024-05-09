using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeModeManager : MonoBehaviour
{
    //スクリプト
    public GameManager _gameManager;

    //制限時間(秒)
    [SerializeField]
    private int _second;

    //1秒ごとを測る数値
    private float _timmerMode;

    //タイマー(テキスト)
    [SerializeField]
    private GameObject _timmerText;

    // Start is called before the first frame update
    void Start()
    {
        _timmerText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Text TimmerText = _timmerText.GetComponent<Text>();

        //残り時間のテキスト表示
        TimmerText.text = "残り:" + _second;

        _timmerMode += Time.deltaTime;

        //1秒ごとにカウントを1減らす
        if(_timmerMode >= 1)
        {
            _second--;
            _timmerMode = 0;
        }

        //残り0秒になったらリザルトを表示する
        if(_second <= 0)
        {
            _gameManager._scoreText.text = "SCORE:" + GameManager._score;
            _gameManager._gameOver.SetActive(true);

            GameManager._stopTrigger = true;
            Time.timeScale = 0f;
        }
    }

    //ゲーム開始時にタイマーを表示させる
    public void StartTimmer()
    {
        _timmerText.SetActive(true);
    }
}
