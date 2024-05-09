using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //イベント用スクリプト
    public SpecialBallSpawn _specialBallSpawn;
    public SkullBallSpawn _skullBallSpawn;
    public JackpotScripts _jackpotScripts;
    public SEManager _seManager;

    //イベント用オブジェクト
    public GameObject _doubleBox, _sideRing, _jackPotTriggerItem;

    //イベント連続発生防止トリガー
    public bool _doubleEventSwitch;
    public bool _sideRingEventSwitch;

    //特定のイベント重複発生防止トリガー
    public bool _jackPotEventSwitch;

    //ジャックポット時の時間ごとのスポーン関連
    private float _jackPotTimmer;
    private int _jackPotCount;

    //ジャックポットアイテムのスポーン範囲(x座標)
    [SerializeField]
    private float _maxJackPot, _minJackPot;

    [SerializeField]
    private Vector3 _spawnJackPos;

    //ジャックポット時に生成する球の個数
    public static int _jackPotCredit;

    //ジャックポット時に生成する球の個数(テキスト)
    [SerializeField]
    public TextMeshProUGUI _jackPotCreditText;

    // Start is called before the first frame update
    void Start()
    {
        _doubleEventSwitch = false;
        _sideRingEventSwitch = false;
        _jackPotEventSwitch = false;

        _jackPotCredit = 100;
        _jackPotCount = 0;

        _doubleBox.SetActive(false);
        _sideRing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _jackPotCreditText.text = "JACKPOT:" + _jackPotCredit;

        //ジャックポット中の処理
        if (_jackPotEventSwitch == true)
        {
            _jackPotTimmer += Time.deltaTime;

            //指定時間ごとに球を生成する
            if(_jackPotTimmer > 0.5)
            {
                _jackpotScripts.JackPot();

                _jackPotTimmer = 0;
                _jackPotCount++;
            }

            //ジャックポットクレジット数分球を生成したら止まる
            if(_jackPotCount >= _jackPotCredit)
            {
                _jackPotCount = 0;
                _jackPotTimmer = 0;
                _jackPotEventSwitch = false;

                _jackPotCount = 100;
            }
        }
    }

    public void Event()
    {
        //乱数
        float _random = UnityEngine.Random.Range(1, 101);
        Debug.Log(_random);

        //乱数結果によってイベントが発生する
        if(_random <= 60)
        {
            NormalEvent();
        }
        else if(_random <= 80)
        {
            //イベント重複時
            if (_doubleEventSwitch == false)
            {
                DoubleUp();

                _doubleEventSwitch = true;
            }
        }
        else if(_random <= 90)
        {

            if (_sideRingEventSwitch == false)
            {
                SideRing();

                _sideRingEventSwitch = true;
            }
        }
        else if( _random <= 95)
        {
            BadSkull();
        }
        else if( _random <= 101)
        {
            JackPotTrigger();
        }
    }

    /// <summary>
    /// ノーマルイベント
    /// 上位のボールを出現させるイベント
    /// </summary>
    public void NormalEvent()
    {
        Debug.Log("NormalEvent");

        for(int i = 0; i < 3; i++)
        {
            _specialBallSpawn.SpecialBall();
        }
    }

    /// <summary>
    /// 2倍チャンスイベント
    /// 1分間Creditが2倍になる箱が生成される
    /// </summary>
    public void DoubleUp()
    {
        Debug.Log("DoubleBoxEvent");
        _doubleBox.SetActive(true);
    }

    /// <summary>
    /// リングミニゲーム
    /// リングにボールを入れると横に溜まってるボールが落ちる
    /// </summary>
    public void SideRing()
    {
        Debug.Log("SideRingEvent");
        _sideRing.SetActive(true);

    }

    /// <summary>
    /// ドクロイベント
    /// ドクロボールを生成する
    /// </summary>
    public void BadSkull()
    {
        Debug.Log("BadSkullEvent");

        if (_jackPotEventSwitch == false)
        {
            _skullBallSpawn.SkullBall();
        }
    }

    /// <summary>
    /// ジャックポットアイテムイベント
    /// ジャックポットアイテムを出現させる
    /// </summary>
    public void JackPotTrigger()
    {
        Debug.Log("JackPotTrigger");
        _jackpotScripts.JackPotTrigger();
    }

    /// <summary>
    /// ジャックポットイベント
    /// 1分以内に宝石を落とせばボールがジャックポットクレジット数分球が生成される
    /// </summary>
    public void JackPot()
    {
        SEManager._audioSource.PlayOneShot(_seManager._jackPotSE);

        _jackPotEventSwitch = true;       
    }
}
