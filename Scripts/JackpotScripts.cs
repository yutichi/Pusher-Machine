using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotScripts : MonoBehaviour
{
    //�{�[��
    [SerializeField]
    private GameObject[] _specialBall;

    //�W���b�N�|�b�g�A�C�e��
    [SerializeField]
    public GameObject[] _jackPotTriggerItem;

    //�X�|�[���n�_�̃����_������(x���W)
    [SerializeField]
    private float _maxX, _minX;

    //�X�|�[���n�_
    [SerializeField]
    private Vector3 _spawnPos;

    public Vector3 JackPotTrigger()
    {
        float x = Random.Range(_minX, _maxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_jackPotTriggerItem[Random.Range(0, _jackPotTriggerItem.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("JackpotBall");

        return new Vector3(x, y, z);
    }

    public Vector3 JackPot()
    {
        float x = Random.Range(_minX, _maxX);
        float y = _spawnPos.y;
        float z = _spawnPos.z;
        Debug.Log(x);

        Instantiate(_specialBall[Random.Range(0, _specialBall.Length)], new Vector3(x, y, z), Quaternion.identity);
        Debug.Log("JackpotBall");

        return new Vector3(x, y, z);
    }
}
