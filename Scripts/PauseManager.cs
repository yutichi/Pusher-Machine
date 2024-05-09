using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    //�X�N���v�g
    public GameManager _gameManager;
    public EventManager _eventManager;
    public SEManager _seManager;

    //�|�[�Y���
    [SerializeField]
    private GameObject _pause;

    //�Z�[�u�m�F���
    [SerializeField]
    private GameObject _checkSave;

    //�Z�[�u�����e�L�X�g
    [SerializeField]
    private GameObject _saveText;

    //�f�[�^�폜�m�F���
    [SerializeField]
    private GameObject _deleteData;

    //�f�[�^�폜�����e�L�X�g
    [SerializeField]
    private GameObject _deleteText;

    //�^�C�g���ɖ߂�m�F���
    [SerializeField]
    private GameObject _checkBackTitle;

    //�Q�[�����J�n���Ă��邩�̔���
    public static bool _pauseStop;

    //�|�[�Y��ʂ��ǂ����̔���
    private bool _pauseTrigger;

    //�Z�[�u�f�[�^�̒��g
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
        //esc����������|�[�Y���(�Q�[�����J�n���Ă���ꍇ)
        if(Input.GetKeyDown(KeyCode.Escape) && _pauseStop == false)
        {
            SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

            _saveText.SetActive(false);
            _deleteText.SetActive(false);

            _pause.SetActive(!_pause.activeSelf);

            //�Q�[�����ĊJ����
            if(_pauseTrigger == true)
            {
                Time.timeScale = 1f;
                _pauseTrigger = false;
            }
            //�Q�[�����~����
            else if(_pauseTrigger == false) 
            { 
                Time.timeScale = 0f;
                _pauseTrigger = true;
            }
        }

    }

    //�Z�[�u(�͂��{�^��)
    public void Save()
    {
        CreditData = _gameManager._credit;
        JackPotCreditData = EventManager._jackPotCredit;
        ScoreData = GameManager._score;

        //�Z�[�u�f�[�^�̑���
        PlayerPrefs.SetInt("DataCheck", _dataCheck + 1);

        //���݂̏�����
        PlayerPrefs.SetInt("Credit",CreditData);

        //���݂̃W���b�N�|�b�g�N���W�b�g
        PlayerPrefs.SetInt("JackPotCredit", JackPotCreditData);

        //���݂̃X�R�A
        PlayerPrefs.SetInt("Score", ScoreData);

        //���j���[��ʂɖ߂�
        BackButton();

        //�Z�[�u�����e�L�X�g�̕\��
        _saveText.SetActive(true);

        //SE
        SEManager._audioSource.PlayOneShot(_seManager._saveSE);
    }

    //�Z�[�u�̊m�F���
    public void SaveCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _checkSave.SetActive(!_checkSave.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //�f�[�^�폜(�͂��{�^��)
    public void DeleteData()
    {
        //�f�[�^�폜
        PlayerPrefs.DeleteAll();

        //���j���[��ʂɖ߂�
        BackButton();

        //�f�[�^�폜�����e�L�X�g�̕\��
        _deleteText.SetActive(true);

        //SE
        SEManager._audioSource.PlayOneShot(_seManager._deleteDataSE);
    }

    //�f�[�^�폜�m�F���
    public void DeleteDataCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _deleteData.SetActive(!_deleteData.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //�^�C�g���ɖ߂�m�F���
    public void BackTitleCheck()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _checkBackTitle.SetActive(!_checkSave.activeSelf);
        _pause.SetActive(!_pause.activeSelf);

        _saveText.SetActive(false);
        _deleteText.SetActive(false);

        _pauseStop = true;
    }

    //�^�C�g���ɖ߂�(�͂�)
    public void BackTitleButton()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(0);
    }

    //�߂�{�^��(������)
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
