using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnd : MonoBehaviour
{
    private int _progress = 0;
    public int _maxProgress = 0;
    private GameObject _currentStage;

    void Start()
    {
        _progress = 0;

        // 현재 스테이지 체크
        _currentStage = GameObject.FindGameObjectWithTag("Stage");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (_progress < _maxProgress)
            {
                _progress += 1;
            }
            else
            {
                _currentStage.GetComponent<StageData>()._isRestarted = true;
                this.gameObject.SetActive(false);

                _progress = 0;
            }
        }
    }
}
