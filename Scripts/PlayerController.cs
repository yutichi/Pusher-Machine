using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //スクリプト
    public GameManager _gameManager;
    public NormalBallSpawn _normalBallSpawn;
    public SEManager _seManager;

    //プレイヤーの球
    [SerializeField]
    private GameObject _playerBall;

    [SerializeField]
    private Transform _playerBallNozzle;

    //移動速度
    [SerializeField]
    private float _speed;

    //移動制限
    [SerializeField]
    private float _playerLeft, _playerRight;

    //クールタイム
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

        //移動制限
        var PlayerPos = transform.position;
        PlayerPos.x = Mathf.Clamp(PlayerPos.x, _playerLeft, _playerRight);
        transform.position = PlayerPos;

        //←又はAで左移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { 
            transform.position -= _speed * transform.right * Time.deltaTime;
        }

        //→又はDで左移動
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += _speed * transform.right * Time.deltaTime;
        }

        //Spaceで球を生成
        if(Input.GetKeyDown(KeyCode.Space) && _coolDown == false)
        {
            //お金が無いなら撃てない
            if(_gameManager._credit <= 0)
            {
                return;
            }

            SEManager._audioSource.PlayOneShot(_seManager._shotSE);

            _coolDown = true;

            //球を生成
            GameObject dropBall = Instantiate(_playerBall) as GameObject;
            dropBall.transform.position = _playerBallNozzle.transform.position;

            //球を打つ度にお金減少
            _gameManager._credit--;

            _normalBallSpawn._neglectTrigger = false;
            _normalBallSpawn.NeglectTimmer = 0;
            _normalBallSpawn._countSpawn++;
        }

        //クールタイム処理
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
