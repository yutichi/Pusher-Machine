using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeModeManager : MonoBehaviour
{
    //�X�N���v�g
    public GameManager _gameManager;

    //��������(�b)
    [SerializeField]
    private int _second;

    //1�b���Ƃ𑪂鐔�l
    private float _timmerMode;

    //�^�C�}�[(�e�L�X�g)
    [SerializeField]
    private GameObject _timmerText;

    // Start is called before the first frame update
    void Start()
    {
        _timmerText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Text TimmerText = _timmerText.GetComponent<Text>();

        //�c�莞�Ԃ̃e�L�X�g�\��
        TimmerText.text = "�c��:" + _second;

        _timmerMode += Time.deltaTime;

        //1�b���ƂɃJ�E���g��1���炷
        if(_timmerMode >= 1)
        {
            _second--;
            _timmerMode = 0;
        }

        //�c��0�b�ɂȂ����烊�U���g��\������
        if(_second <= 0)
        {
            _gameManager._scoreText.text = "SCORE:" + GameManager._score;
            _gameManager._gameOver.SetActive(true);

            GameManager._stopTrigger = true;
            Time.timeScale = 0f;
        }
    }

    //�Q�[���J�n���Ƀ^�C�}�[��\��������
    public void StartTimmer()
    {
        _timmerText.SetActive(true);
    }
}
