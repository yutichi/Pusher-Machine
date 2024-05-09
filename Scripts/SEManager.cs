using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField]
    public AudioClip _shotSE
                    ,_buttonSE
                    ,_boxSE
                    ,_doubleBoxSE
                    ,_jackPotSE
                    ,_sideRingSE
                    ,_skullBallSE
                    ,_saveSE
                    ,_deleteDataSE
                    ,_gameOverSE
                    ,_resultSE;

    public static AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
