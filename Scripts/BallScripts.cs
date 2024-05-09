using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    //スクリプト
    public GameManager _gameManager;
    public EventManager _eventManager;
    public JackpotScripts _jackpotScripts;
    public SEManager _seManager;

    //得点倍率(ボール)
    [SerializeField]
    private int _magnificationBall;

    //得点倍率(スコア)
    [SerializeField]
    private int _magnificationScore;

    //SE用トリガー
    [SerializeField]
    private bool SETrigger;

    //各ボールの処理
    void OnTriggerEnter(Collider other)
    {
        //プレイヤーボール
        if(other.gameObject.tag == "PlayerBall") 
        {
            //デバッグログ
            Debug.Log("PlayerBall");

            //お金増加
            _gameManager._credit = _gameManager._credit + 1 * _magnificationBall;

            //スコア増加
            GameManager._score = GameManager._score + 100 * _magnificationScore;

            //SE
            BoxSE();

            //オブジェクト削除
            Destroy(other.gameObject);
        }

        //普通のボール
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Ball");

            _gameManager._credit = _gameManager._credit + 3 * _magnificationBall;
            GameManager._score = GameManager._score + 300 * _magnificationScore;

            //ジャックポット数を増加
            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 1;
            }

            BoxSE();

            Destroy(other.gameObject);
        }

        //イベントボックス
        if (other.gameObject.tag == "EventBox")
        {
            //デバッグログ
            Debug.Log("EventBox");

            _eventManager.Event();
            _gameManager._credit = _gameManager._credit + 1 * _magnificationBall;
            GameManager._score = GameManager._score + 100 * _magnificationScore;

            BoxSE();

            Destroy(other.gameObject);
        }

        //ガラスのボール
        if (other.gameObject.tag == "GlassBall")
        {
            Debug.Log("GlassBall");

            _gameManager._credit = _gameManager._credit + 5 * _magnificationBall;
            GameManager._score = GameManager._score + 500 * _magnificationScore;

            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 1;
            }

            BoxSE();

            Destroy(other.gameObject);
        }

        //スーパーボール
        if (other.gameObject.tag == "SuperBall")
        {
            Debug.Log("SuperBall");

            _gameManager._credit = _gameManager._credit + 10 * _magnificationBall;
            GameManager._score = GameManager._score + 1000 * _magnificationScore;

            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 1;
            }

            BoxSE();

            Destroy(other.gameObject);
        }

        //ドクロボール
        if(other.gameObject.tag == "SkullBall")
        {
            Debug.Log("SkullBall");

            _gameManager._credit = _gameManager._credit - 5;
            GameManager._score = GameManager._score - 500;

            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 3;
            }

            SEManager._audioSource.PlayOneShot(_seManager._skullBallSE);

            Destroy(other.gameObject);
        }

        //ゴールドボール
        if (other.gameObject.tag == "GoldBall")
        {
            Debug.Log("GoldBall");

            _gameManager._credit = _gameManager._credit + 30 * _magnificationBall;
            GameManager._score = GameManager._score + 3000 * _magnificationScore;

            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 5;
            }

            BoxSE();

            Destroy(other.gameObject);
        }

        //ジャックポットアイテム
        if (other.gameObject.tag == "JackpotItem")
        {
            Debug.Log("JackpotItem");

            _eventManager.JackPot();

            BoxSE();

            Destroy(other.gameObject);
        }
    }

    //球が入ったときにSEを鳴らす
    void BoxSE()
    {
        //SE
        if (SETrigger == false)
        {
            //通常箱SE
            SEManager._audioSource.PlayOneShot(_seManager._boxSE);
        }
        else if (SETrigger == true)
        {
            //2倍ボックスSE
            SEManager._audioSource.PlayOneShot(_seManager._doubleBoxSE);
        }
    }
}
