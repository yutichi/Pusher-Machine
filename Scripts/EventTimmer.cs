using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventTimmer : MonoBehaviour
{
    //スクリプト
    public EventManager _eventManager;
    public JackpotScripts _jackPotScripts;

    //イベントタイマー
    private float _timmer;

    //非表示用bool
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

        //時間切れになったら消滅させる
        if (_timmer <= 0)
        {
            _timmer = 60f;

            //2倍ボックス
            if (_doubleBoxSwitch == true)
            {
                _eventManager._doubleEventSwitch = false;
                _eventManager._doubleBox.SetActive(false);
            }
            //リング
            if(_eventManager._sideRingEventSwitch == true)
            {
                _eventManager._sideRingEventSwitch = false;
                _eventManager._sideRing.SetActive(false);
            }
        }
    }
}
