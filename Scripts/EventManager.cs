using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //�C�x���g�p�X�N���v�g
    public SpecialBallSpawn _specialBallSpawn;
    public SkullBallSpawn _skullBallSpawn;
    public JackpotScripts _jackpotScripts;
    public SEManager _seManager;

    //�C�x���g�p�I�u�W�F�N�g
    public GameObject _doubleBox, _sideRing, _jackPotTriggerItem;

    //�C�x���g�A�������h�~�g���K�[
    public bool _doubleEventSwitch;
    public bool _sideRingEventSwitch;

    //����̃C�x���g�d�������h�~�g���K�[
    public bool _jackPotEventSwitch;

    //�W���b�N�|�b�g���̎��Ԃ��Ƃ̃X�|�[���֘A
    private float _jackPotTimmer;
    private int _jackPotCount;

    //�W���b�N�|�b�g�A�C�e���̃X�|�[���͈�(x���W)
    [SerializeField]
    private float _maxJackPot, _minJackPot;

    [SerializeField]
    private Vector3 _spawnJackPos;

    //�W���b�N�|�b�g���ɐ������鋅�̌�
    public static int _jackPotCredit;

    //�W���b�N�|�b�g���ɐ������鋅�̌�(�e�L�X�g)
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

        //�W���b�N�|�b�g���̏���
        if (_jackPotEventSwitch == true)
        {
            _jackPotTimmer += Time.deltaTime;

            //�w�莞�Ԃ��Ƃɋ��𐶐�����
            if(_jackPotTimmer > 0.5)
            {
                _jackpotScripts.JackPot();

                _jackPotTimmer = 0;
                _jackPotCount++;
            }

            //�W���b�N�|�b�g�N���W�b�g�������𐶐�������~�܂�
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
        //����
        float _random = UnityEngine.Random.Range(1, 101);
        Debug.Log(_random);

        //�������ʂɂ���ăC�x���g����������
        if(_random <= 60)
        {
            NormalEvent();
        }
        else if(_random <= 80)
        {
            //�C�x���g�d����
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
    /// �m�[�}���C�x���g
    /// ��ʂ̃{�[�����o��������C�x���g
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
    /// 2�{�`�����X�C�x���g
    /// 1����Credit��2�{�ɂȂ锠�����������
    /// </summary>
    public void DoubleUp()
    {
        Debug.Log("DoubleBoxEvent");
        _doubleBox.SetActive(true);
    }

    /// <summary>
    /// �����O�~�j�Q�[��
    /// �����O�Ƀ{�[��������Ɖ��ɗ��܂��Ă�{�[����������
    /// </summary>
    public void SideRing()
    {
        Debug.Log("SideRingEvent");
        _sideRing.SetActive(true);

    }

    /// <summary>
    /// �h�N���C�x���g
    /// �h�N���{�[���𐶐�����
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
    /// �W���b�N�|�b�g�A�C�e���C�x���g
    /// �W���b�N�|�b�g�A�C�e�����o��������
    /// </summary>
    public void JackPotTrigger()
    {
        Debug.Log("JackPotTrigger");
        _jackpotScripts.JackPotTrigger();
    }

    /// <summary>
    /// �W���b�N�|�b�g�C�x���g
    /// 1���ȓ��ɕ�΂𗎂Ƃ��΃{�[�����W���b�N�|�b�g�N���W�b�g�����������������
    /// </summary>
    public void JackPot()
    {
        SEManager._audioSource.PlayOneShot(_seManager._jackPotSE);

        _jackPotEventSwitch = true;       
    }
}
