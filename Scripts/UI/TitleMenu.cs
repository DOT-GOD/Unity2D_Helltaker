using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public GameObject _nextScene;                    // �����ʿ� : ���� ��� ������Ʈ
    public GameObject _chapterChoice;                // �����ʿ� : é�ͼ������ ������Ʈ
    public GameObject _title;                        // �����ʿ� : Ÿ��Ʋȭ�� ������Ʈ

    public Text _dialogueText;                       // �����ʿ� : ��ȭ���� ǥ���� �ؽ�Ʈ ������Ʈ
    public GameObject _selectSFX;                   // �����ʿ� : ���� ȿ����

    private SelectMenu _select;

    private bool _isWaiting = false;

    void Start()
    {
        _select = this.gameObject.GetComponentInChildren<SelectMenu>();
        _dialogueText.text = "";
    }
    void Update()
    {
        // ������������
        if (_isWaiting)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if(_select._num == 0)   // ���ӽ���
            {
                _nextScene.SetActive(true);
                _title.gameObject.SetActive(false);
            }
            else if (_select._num == 1)     // é�ͼ���
            {
                _select._isPaused = true;

                for (int i = 0; i < 3; i++)
                {
                    _select._menu[i].SetActive(false);
                }
                _chapterChoice.SetActive(true);
            }
            else if (_select._num == 2)     // ��������
            {
                _isWaiting = true;
                Invoke("Quit", 4.0f);
            }

            // ȿ����
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
