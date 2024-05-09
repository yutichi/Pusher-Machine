using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float _jackPotTimmer = 60f;

    [SerializeField]
    private bool _jackPotTriggerItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_jackPotTriggerItem == true) 
        { 
            _jackPotTimmer -= Time.deltaTime;
        }

        //負荷対策
        if (this.transform.position.y <= -15)
        {
            Destroy(gameObject);
        }

        //60秒経つと消滅(ジャックポットアイテム専用)
        if(_jackPotTimmer <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
