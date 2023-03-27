using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDialogue : MonoBehaviour
{
    void Start()
    {
        GameObject _currentStage = GameObject.FindGameObjectWithTag("Stage");
        StageData _data = _currentStage.GetComponent<StageData>();
        _data._isExit = true;
    }

    void Update()
    {
        
    }
}
