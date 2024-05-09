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

        //���ב΍�
        if (this.transform.position.y <= -15)
        {
            Destroy(gameObject);
        }

        //60�b�o�Ə���(�W���b�N�|�b�g�A�C�e����p)
        if(_jackPotTimmer <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
