using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventTimmer : MonoBehaviour
{
    //�X�N���v�g
    public EventManager _eventManager;
    public JackpotScripts _jackPotScripts;

    //�C�x���g�^�C�}�[
    private float _timmer;

    //��\���pbool
    [SerializeField]
    private bool _doubleBoxSwitch, _sideRingSwitch;

    // Start is called before the first frame update
    void Start()
    {
        _timmer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        _timmer -= Time.deltaTime;

        //���Ԑ؂�ɂȂ�������ł�����
        if (_timmer <= 0)
        {
            _timmer = 60f;

            //2�{�{�b�N�X
            if (_doubleBoxSwitch == true)
            {
                _eventManager._doubleEventSwitch = false;
                _eventManager._doubleBox.SetActive(false);
            }
            //�����O
            if(_eventManager._sideRingEventSwitch == true)
            {
                _eventManager._sideRingEventSwitch = false;
                _eventManager._sideRing.SetActive(false);
            }
        }
    }
}
