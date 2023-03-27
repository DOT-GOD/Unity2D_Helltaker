using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private SelectMenu _select;

    public Text _dialogueText;                       // 지정필요 : 대화문을 표시할 텍스트 오브젝트
    public GameObject _selectSFX;                   // 지정필요 : 선택 효과음

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
            if (_select._num == 0)   // 게임종료
            {
                _isWaiting = true;
                Invoke("Quit", 4.0f);
            }


            // 효과음
            GameObject _sound = new GameObject();
            SoundManager _soundManager = _sound.gameObject.AddComponent<SoundManager>();
            _soundManager.Play(_selectSFX);
        }
    }

    private void Quit()
    {
        Application.Quit();
    }
}
