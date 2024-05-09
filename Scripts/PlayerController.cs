using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�N���v�g
    public GameManager _gameManager;
    public NormalBallSpawn _normalBallSpawn;
    public SEManager _seManager;

    //�v���C���[�̋�
    [SerializeField]
    private GameObject _playerBall;

    [SerializeField]
    private Transform _playerBallNozzle;

    //�ړ����x
    [SerializeField]
    private float _speed;

    //�ړ�����
    [SerializeField]
    private float _playerLeft, _playerRight;

    //�N�[���^�C��
    private float _coolTime = 1.0f;
    private bool _coolDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager._stopTrigger == true)
        {
            return;
        }

        //�ړ�����
        var PlayerPos = transform.position;
        PlayerPos.x = Mathf.Clamp(PlayerPos.x, _playerLeft, _playerRight);
        transform.position = PlayerPos;

        //������A�ō��ړ�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { 
            transform.position -= _speed * transform.right * Time.deltaTime;
        }

        //������D�ō��ړ�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += _speed * transform.right * Time.deltaTime;
        }

        //Space�ŋ��𐶐�
        if(Input.GetKeyDown(KeyCode.Space) && _coolDown == false)
        {
            //�����������Ȃ猂�ĂȂ�
            if(_gameManager._credit <= 0)
            {
                return;
            }

            SEManager._audioSource.PlayOneShot(_seManager._shotSE);

            _coolDown = true;

            //���𐶐�
            GameObject dropBall = Instantiate(_playerBall) as GameObject;
            dropBall.transform.position = _playerBallNozzle.transform.position;

            //����łx�ɂ�������
            _gameManager._credit--;

            _normalBallSpawn._neglectTrigger = false;
            _normalBallSpawn.NeglectTimmer = 0;
            _normalBallSpawn._countSpawn++;
        }

        //�N�[���^�C������
        if(_coolDown == true)
        {
            _coolTime -= Time.deltaTime;

            if(_coolTime <= 0)
            {
                _coolTime = 1.2f;
                _coolDown = false;
            }
        }
    }
}
