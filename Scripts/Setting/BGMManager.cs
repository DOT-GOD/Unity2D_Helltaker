using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject _openingBGM;
    public GameObject _stageBGM;

    void Start()
    {
    }

    void Update()
    {
        GameObject _stage = null;
        _stage = GameObject.FindGameObjectWithTag("Stage");

        // 스테이지 활성화 여부에 따라 음악 선택
        if (_stage == null)
        {
            _openingBGM.SetActive(true);
            _stageBGM.SetActive(false);
        }
        else
        {
            _openingBGM.SetActive(false);
            _stageBGM.SetActive(true);
        }

    }
}
