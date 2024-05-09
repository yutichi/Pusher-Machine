using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRingEvent : MonoBehaviour
{
    //�X�N���v�g
    public SEManager _seManager;

    //�J�����E�̋�
    [SerializeField]
    private GameObject _openLeftGrass, _openRightGrass;

    //�g���K�[
    private bool _openSideTrigger;

    //�A�j���[�V����
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
                //�����̃K���X�����
                _openLeftGrass.SetActive(true);
                _openRightGrass.SetActive(true);

                //�O���̃K���X�̃A�j���[�V����
                _leftGrassAnim.SetBool("LeftSwitch", false);
                _rightGrassAnim.SetBool("RightSwitch", false);

                _openSideTrigger = false;
            }));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����̃K���X���J����
        _openLeftGrass.SetActive(false);
        _openRightGrass.SetActive(false);

        //�O���̃K���X�̃A�j���[�V����
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
