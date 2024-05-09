using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�X�N���v�g
    public EventManager _eventManager;
    public SEManager _seManager;

    //����
    [SerializeField]
    public int _credit;

    //����(�e�L�X�g)
    [SerializeField]
    public TextMeshProUGUI _creditText;

    //�X�R�A
    public static int _score;

    //�X�R�A(�e�L�X�g)
    [SerializeField]
    public TextMeshProUGUI _scoreText;

    //�^�C�g���֘A
    [SerializeField]
    private GameObject _title, _creditActive, _jackPotActive;

    //�Q�[�����[�h�I�����
    [SerializeField]
    private GameObject _modeScene;

    //�Q�[���I�[�o�[�֘A
    [SerializeField]
    public GameObject _gameOver;
    private float _gameOverTimmer;
    private bool _gameOverTrigger;

    //�Q�[���O���̃g���K�[
    public static bool _stopTrigger;

    //�f�[�^�����邩�̊m�F
    private int _saveDataCheck;

    //�Q�[���I�[�o�[�A���U���g����SE�p�g���K�[
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

        //�����������Ȃ�����ۂ̏���
        if (_credit <= 0) 
        {
            _gameOverTrigger = true;

            //10�b�҂��Ă�����������Ώ���
            if(_gameOverTimmer <= 0)
            {
                //�Q�[���I�[�o�[
                _scoreText.text = "SCORE:" + _score;
                _gameOver.SetActive(true);

                _stopTrigger = true;

                //���U���gSE
                if(_resultSETrigger == true) 
                {
                    //�ʏ탂�[�h��
                    SEManager._audioSource.PlayOneShot(_seManager._gameOverSE);
                }
                else if(_resultSETrigger == false)
                {
                    //���ꃂ�[�h��
                    SEManager._audioSource.PlayOneShot(_seManager._resultSE);
                }

                Time.timeScale = 0f;
            }
        }
        else if (_credit >= 1) 
        {
            _gameOverTrigger = false;
        }

        //�����������Ȃ�����10�b�҂�
        if(_gameOverTrigger == false)
        {
            _gameOverTimmer = 10f;
        }
        else if(_gameOverTrigger == true) 
        {
            _gameOverTimmer -= Time.deltaTime;
        }
    }

    //���[�h�I����ʂɈڍs����
    public void ModeScene()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        _title.SetActive(false);
        _modeScene.SetActive(true);

        if(_saveDataCheck > 0)
        {
            //�f�[�^�����[�h����
            _credit = PlayerPrefs.GetInt("Credit");
            EventManager._jackPotCredit = PlayerPrefs.GetInt("JackPotCredit");
            _score = PlayerPrefs.GetInt("Score");
        }
    }

    //���Ԑ������[�h
    public void TimeMode()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(1);
    }

    //���������[�h
    public void RemainingMode()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        SceneManager.LoadScene(2);
    }

    //�Q�[���X�^�[�g�{�^��
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

    //���X�|�[���{�^��
    public void Respawn()
    {
        SEManager._audioSource.PlayOneShot(_seManager._buttonSE);

        //�X�R�A�A�W���b�N�|�b�g�N���W�b�g�A���������Z�b�g�����
        _score = 0;
        _credit = 100;
        EventManager._jackPotCredit = 100;

        //�Q�[���ăX�^�[�g
        _gameOver.SetActive(false);

        _stopTrigger = false;
        Time.timeScale = 1f;
    }

    //�^�C�g���ɖ߂�(�T�u���[�h�p)
    public void BackTitleButton()
    {
        SceneManager.LoadScene(0);
    }
}
