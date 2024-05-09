using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotScripts : MonoBehaviour
{
    //ボール
    [SerializeField]
    private GameObject[] _specialBall;

    //ジャックポットアイテム
    [SerializeField]
    public GameObject[] _jackPotTriggerItem;

    //スポーン地点のランダム部分(x座標)
    [SerializeField]
    private float _maxX, _minX;

    //スポーン地点
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
