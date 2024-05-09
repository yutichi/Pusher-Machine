using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRingEvent : MonoBehaviour
{
    //スクリプト
    public SEManager _seManager;

    //開く左右の鏡
    [SerializeField]
    private GameObject _openLeftGrass, _openRightGrass;

    //トリガー
    private bool _openSideTrigger;

    //アニメーション
    [SerializeField]
    private Animator _leftGrassAnim, _rightGrassAnim;

    // Start is called before the first frame update
    void Start()
    {
        _openSideTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_openSideTrigger == true)
        {
            StartCoroutine(DelayCoroutine(5.5f, () =>
            {
                //内側のガラスを閉じる
                _openLeftGrass.SetActive(true);
                _openRightGrass.SetActive(true);

                //外側のガラスのアニメーション
                _leftGrassAnim.SetBool("LeftSwitch", false);
                _rightGrassAnim.SetBool("RightSwitch", false);

                _openSideTrigger = false;
            }));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //内側のガラスを開ける
        _openLeftGrass.SetActive(false);
        _openRightGrass.SetActive(false);

        //外側のガラスのアニメーション
        _leftGrassAnim.SetBool("LeftSwitch", true);
        _rightGrassAnim.SetBool("RightSwitch", true);

        _openSideTrigger = true;

        //SE
        SEManager._audioSource.PlayOneShot(_seManager._sideRingSE);
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
