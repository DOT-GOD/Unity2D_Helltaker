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

        // �������� Ȱ��ȭ ���ο� ���� ���� ����
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
