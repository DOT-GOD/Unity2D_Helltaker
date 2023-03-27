using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public GameObject _nextScene;                    // 지정필요 : 다음 장면 오브젝트
    public GameObject _chapterChoice;                // 지정필요 : 챕터선택장면 오브젝트
    public GameObject _title;                        // 지정필요 : 타이틀화면 오브젝트

    public Text _dialogueText;                       // 지정필요 : 대화문을 표시할 텍스트 오브젝트
    public GameObject _selectSFX;                   // 지정필요 : 선택 효과음

    private SelectMenu _select;

    private bool _isWaiting = false;

    void Start()
    {
        _select = this.gameObject.GetComponentInChildren<SelectMenu>();
        _dialogueText.text = "";
    }
    void Update()
    {
        // 게임종료대기중
        if (_isWaiting)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if(_select._num == 0)   // 게임시작
            {
                _nextScene.SetActive(true);
                _title.gameObject.SetActive(false);
            }
            else if (_select._num == 1)     // 챕터선택
            {
                _select._isPaused = true;

                for (int i = 0; i < 3; i++)
                {
                    _select._menu[i].SetActive(false);
                }
                _chapterChoice.SetActive(true);
            }
            else if (_select._num == 2)     // 게임종료
            {
                _isWaiting = true;
                Invoke("Quit", 4.0f);
            }

            // 효과음
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_selectSFX);
        }
    }

    private void Quit()
    {
        Application.Quit();
    }
}
