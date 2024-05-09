using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScripts : MonoBehaviour
{
    //床に当たったオブジェクト削除(負荷対策)
    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
