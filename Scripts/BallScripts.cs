using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    //�X�N���v�g
    public GameManager _gameManager;
    public EventManager _eventManager;
    public JackpotScripts _jackpotScripts;
    public SEManager _seManager;

    //���_�{��(�{�[��)
    [SerializeField]
    private int _magnificationBall;

    //���_�{��(�X�R�A)
    [SerializeField]
    private int _magnificationScore;

    //SE�p�g���K�[
    [SerializeField]
    private bool SETrigger;

    //�e�{�[���̏���
    void OnTriggerEnter(Collider other)
    {
        //�v���C���[�{�[��
        if(other.gameObject.tag == "PlayerBall") 
        {
            //�f�o�b�O���O
            Debug.Log("PlayerBall");

            //��������
            _gameManager._credit = _gameManager._credit + 1 * _magnificationBall;

            //�X�R�A����
            GameManager._score = GameManager._score + 100 * _magnificationScore;

            //SE
            BoxSE();

            //�I�u�W�F�N�g�폜
            Destroy(other.gameObject);
        }

        //���ʂ̃{�[��
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Ball");

            _gameManager._credit = _gameManager._credit + 3 * _magnificationBall;
            GameManager._score = GameManager._score + 300 * _magnificationScore;

            //�W���b�N�|�b�g���𑝉�
            if (_eventManager._jackPotEventSwitch == false)
            {
                EventManager._jackPotCredit = EventManager._jackPotCredit + 1;
            }

            BoxSE();

            Destroy(other.gameObject);
        }

        //�C�x���g�{�b�N�X
        if (other.gameObject.tag == "EventBox")
        {
            //�f�o�b�O���O
            Debug.Log("EventBox");

            _eventManager.Event();
            _gameManager._credit = _gameManager._credit + 1 * _magnificationBall;
            GameManager._score = GameManager._score + 100 * _magnificationScore;

            BoxSE();

            Destroy(other.gameObject);
        }

        //�K���X�̃{�[��
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

        //�X�[�p�[�{�[��
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

        //�h�N���{�[��
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

        //�S�[���h�{�[��
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

        //�W���b�N�|�b�g�A�C�e��
        if (other.gameObject.tag == "JackpotItem")
        {
            Debug.Log("JackpotItem");

            _eventManager.JackPot();

            BoxSE();

            Destroy(other.gameObject);
        }
    }

    //�����������Ƃ���SE��炷
    void BoxSE()
    {
        //SE
        if (SETrigger == false)
        {
            //�ʏ픠SE
            SEManager._audioSource.PlayOneShot(_seManager._boxSE);
        }
        else if (SETrigger == true)
        {
            //2�{�{�b�N�XSE
            SEManager._audioSource.PlayOneShot(_seManager._doubleBoxSE);
        }
    }
}
