using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScripts : MonoBehaviour
{
    //���ɓ��������I�u�W�F�N�g�폜(���ב΍�)
    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
