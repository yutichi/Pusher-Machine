using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBallSpawn : MonoBehaviour
{
    //�X�y�V�����{�[��
    [SerializeField]
    private GameObject[] _specialBall;

    //�X�|�[���n�_�̃����_������(x���W)
    [SerializeField]
    private float _maxX, _minX;

    //�X�|�[���n�_
    [SerializeField]
    private Vector3 _spawnPos;

    public Vector3 SpecialBall()
    {
        float x = Random.Range(_minX, _maxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_specialBall[Random.Range(0, _specialBall.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("SpecialBall");

        return new Vector3(x, y, z);
    }
}
