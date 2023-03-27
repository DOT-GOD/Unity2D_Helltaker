using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveDialogue : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    public int _correctChoice = 0;                  // 지정필요 : 올바른 선택지

    public GameObject _winScene;                    // 지정필요 : 승리 장면 오브젝트
    public GameObject _deadScene;                   // 지정필요 : 사망 장면 오브젝트
    public GameObject _loveDialogue;                // 지정필요 : 현재화면 오브젝트
    public GameObject _selectSFX;                   // 지정필요 : 선택 효과음

    private SelectMenu _select;

    void Start()
    {
        _select = this.gameObject.GetComponentInChildren<SelectMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.GetComponentInParent<DialogueProgress>().Initialize();
            // 올바른 선택지
            if (_select._num == _correctChoice)
            {
                _winScene.SetActive(true);
                _loveDialogue.gameObject.SetActive(false);
            }
            // 틀린 선택지
            else
            {
                _deadScene.SetActive(true);
                _loveDialogue.gameObject.SetActive(false);
            }

            // 효과음
            GameObject _sound = new GameObject();
            SoundManager _soundManager = _sound.gameObject.AddComponent<SoundManager>();
            _soundManager.Play(_selectSFX);
        }
    }
}
