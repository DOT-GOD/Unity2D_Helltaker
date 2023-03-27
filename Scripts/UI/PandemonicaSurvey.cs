using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PandemonicaSurvey : MonoBehaviour
{
    public int _scoreNum = 1;
    public Text _scoreText;                      // 지정필요 : 점수 텍스트

    public GameObject _currentScene;
    public GameObject _scoreTenScene;
    public GameObject _otherScene;

    void Start()
    {
        
    }

    void Update()
    {
        _scoreText.text = "" + _scoreNum;

        // 좌우로 점수조정
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_scoreNum > 1)
                _scoreNum -= 1;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_scoreNum < 10)
                _scoreNum += 1;
        }

        // 선택완료
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (_scoreNum == 10)
                _scoreTenScene.SetActive(true);
            else
                _otherScene.SetActive(true);

            _currentScene.SetActive(false);
            this.gameObject.SetActive(false);
            _currentScene.GetComponent<DialogueProgress>()._progress = 0;
            _currentScene.GetComponent<DialogueProgress>().Initialize();
        }

    }
}
