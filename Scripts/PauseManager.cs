using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    //スクリプト
    public GameManager _gameManager;
    public EventManager _eventManager;
    public SEManager _seManager;

    //ポーズ画面
    [SerializeField]
    private GameObject _pause;

    //セーブ確認画面
    [SerializeField]
    private GameObject _checkSave;

    //セーブ完了テキスト
    [SerializeField]
    private GameObject _saveText;

    //データ削除確認画面
    [SerializeField]
    private GameObject _deleteData;

    //データ削除完了テキスト
    [SerializeField]
    private GameObject _deleteText;

    //タイトルに戻る確認画面
    [SerializeField]
    private GameObject _checkBackTitle;

    //ゲームが開始しているかの判定
    public static bool _pauseStop;

    //ポーズ画面がどうかの判定
    private bool _pauseTrigger;

    //セーブデータの中身
    public static int _dataCheck;
    public static int CreditData;
    public static int JackPotCreditData;
    public static int ScoreData;

    // Start is called before the first frame update
    void Start()
    {
        _pause.SetActive(false);
        _checkSave.SetActive(false);
        _saveText.SetActive(false);
        _deleteData.SetActive(false);
        _deleteText.SetActive(false);
        _checkBackTitle.SetActive(false);

        _pauseStop = true;
        _pauseTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //escを押したらポーズ画面(ゲームが開始している場合)
        if(Input.GetKeyDown(KeyCode.Escape) && _pauseStop == false)
        {
            SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

            _saveText.SetActive(false);
            _deleteText.SetActive(false);

            _pause.SetActive(!_pause.activeSelf);

            //ゲームを再開する
            if(_pauseTrigger == true)
            {
                Time.timeScale = 1f;
                _pauseTrigger = false;
            }
            //ゲームを停止する
            else if(_pauseTrigger == false) 
            { 
                Time.timeScale = 0f;
                _pauseTrigger = true;
            }
        }

    }

    //セーブ(はいボタン)
    public void Save()
    {
        CreditData = _gameManager._credit;
        JackPotCreditData = EventManager._jackPotCredit;
        ScoreData = GameManager._score;

        //セーブデータの存在
        PlayerPrefs.SetInt("DataCheck", _dataCheck + 1);

        //現在の所持金
        PlayerPrefs.SetInt("Credit",CreditData);

        //現在のジャックポットクレジット
        PlayerPrefs.SetInt("JackPotCredit", JackPotCreditData);

        //現在のスコア
        PlayerPrefs.SetInt("Score", ScoreData);

        //メニュー画面に戻る
        BackButton();

        //セーブ完了テキストの表示
        _saveText.SetActive(true);

        //SE
        SEManager._audioSource.PlayOneShot(_seManager._saveSE);
    }

    //セーブの確認画面
    public void SaveCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _checkSave.SetActive(!_checkSave.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //データ削除(はいボタン)
    public void DeleteData()
    {
        //データ削除
        PlayerPrefs.DeleteAll();

        //メニュー画面に戻る
        BackButton();

        //データ削除完了テキストの表示
        _deleteText.SetActive(true);

        //SE
        SEManager._audioSource.PlayOneShot(_seManager._deleteDataSE);
    }

    //データ削除確認画面
    public void DeleteDataCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _deleteData.SetActive(!_deleteData.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //タイトルに戻る確認画面
    public void BackTitleCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _checkBackTitle.SetActive(!_checkSave.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //タイトルに戻る(はい)
    public void BackTitleButton()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(0);
    }

    //戻るボタン(いいえ)
    public void BackButton()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _pause.SetActive(true);
        _checkSave.SetActive(false);
        _deleteData.SetActive(false);
        _checkBackTitle.SetActive(false);

        _pauseStop = false;
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
