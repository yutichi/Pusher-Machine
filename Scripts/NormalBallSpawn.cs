using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalBallSpawn : MonoBehaviour
{
    //通常のボール
    [SerializeField]
    private GameObject[] _normalBall;

    //球の回数のスポーン
    public int _countSpawn;

    //一定時間のスポーン
    [SerializeField]
    private float _timmerSpawn;
    private float _timmer;

    //スポーン地点
    [SerializeField]
    private Vector3 _spawnPos;

    //スポーン地点のランダム部分(x座標、左のガラス用)
    [SerializeField]
    private float _leftMaxX, _leftMinX;

    //スポーン地点のランダム部分(x座標、右のガラス用)
    [SerializeField]
    private float _rightMaxX, _rightMinX;

    //スポーン地点のランダム部分(x座標、タイマー用)
    [SerializeField]
    private float _timmerMaxX, _timmerMinX;

    //放置防止トリガー
    public bool _neglectTrigger;

    //放置時間
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
        //放置中は全て機能停止
        if(_neglectTrigger == true)
        {
            return;
        }

        _timmer += Time.deltaTime;
        NeglectTimmer += Time.deltaTime;

        //一定時間毎にカラーボールを落とす
        if(_timmer >= _timmerSpawn)
        {
            RandomSpawnTimmer();
        }

        //5球ずつ落とす度カラーボールをサイドのガラスに補充する
        if (_countSpawn >= 5)
        {
            //左右を決める乱数
            float _countRandom = Random.Range(1, 100);

            //左
            if(_countRandom <= 50)
            {
                RandomSpawnCountLeft();
            }
            //右
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

    //一定時間毎のスポーン
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

    //サイドのガラスガラスのスポーン(左)
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

    //サイドのガラスのスポーン(右)
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
