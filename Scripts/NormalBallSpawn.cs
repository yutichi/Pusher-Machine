using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalBallSpawn : MonoBehaviour
{
    //�ʏ�̃{�[��
    [SerializeField]
    private GameObject[] _normalBall;

    //���̉񐔂̃X�|�[��
    public int _countSpawn;

    //��莞�Ԃ̃X�|�[��
    [SerializeField]
    private float _timmerSpawn;
    private float _timmer;

    //�X�|�[���n�_
    [SerializeField]
    private Vector3 _spawnPos;

    //�X�|�[���n�_�̃����_������(x���W�A���̃K���X�p)
    [SerializeField]
    private float _leftMaxX, _leftMinX;

    //�X�|�[���n�_�̃����_������(x���W�A�E�̃K���X�p)
    [SerializeField]
    private float _rightMaxX, _rightMinX;

    //�X�|�[���n�_�̃����_������(x���W�A�^�C�}�[�p)
    [SerializeField]
    private float _timmerMaxX, _timmerMinX;

    //���u�h�~�g���K�[
    public bool _neglectTrigger;

    //���u����
    private float _neglectTimmer = 7f;
    public float NeglectTimmer;

    // Start is called before the first frame update
    void Start()
    {
        _timmer = 0;
        NeglectTimmer = 0;

        _neglectTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        //���u���͑S�ċ@�\��~
        if(_neglectTrigger == true)
        {
            return;
        }

        _timmer += Time.deltaTime;
        NeglectTimmer += Time.deltaTime;

        //��莞�Ԗ��ɃJ���[�{�[���𗎂Ƃ�
        if(_timmer >= _timmerSpawn)
        {
            RandomSpawnTimmer();
        }

        //5�������Ƃ��x�J���[�{�[�����T�C�h�̃K���X�ɕ�[����
        if (_countSpawn >= 5)
        {
            //���E�����߂闐��
            float _countRandom = Random.Range(1, 100);

            //��
            if(_countRandom <= 50)
            {
                RandomSpawnCountLeft();
            }
            //�E
            else
            {
                RandomSpawnCountRight();
            }
        }

        if (NeglectTimmer >= _neglectTimmer) 
        {
            _neglectTrigger = true;
        }
    }

    //��莞�Ԗ��̃X�|�[��
    private Vector3 RandomSpawnTimmer()
    {
        float x = Random.Range(_timmerMinX, _timmerMaxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_normalBall[Random.Range(0, _normalBall.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("NormalSpawnTimmer");

        _timmer = 0f;

        return new Vector3(x, y, z);
    }

    //�T�C�h�̃K���X�K���X�̃X�|�[��(��)
    private Vector3 RandomSpawnCountLeft()
    {
        float x = Random.Range(_leftMinX, _leftMaxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_normalBall[Random.Range(0, _normalBall.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("NormalSpawnCountLeft");

        _countSpawn = 0;

        return new Vector3(x, y, z);
    }

    //�T�C�h�̃K���X�̃X�|�[��(�E)
    private Vector3 RandomSpawnCountRight()
    {
        float x = Random.Range(_rightMinX, _rightMaxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_normalBall[Random.Range(0, _normalBall.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("NormalSpawnCountRight");

        _countSpawn = 0;

        return new Vector3(x, y, z);
    }
}
