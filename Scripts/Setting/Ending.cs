using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private SelectMenu _select;

    public Text _dialogueText;                       // �����ʿ� : ��ȭ���� ǥ���� �ؽ�Ʈ ������Ʈ
    public GameObject _selectSFX;                   // �����ʿ� : ���� ȿ����

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
            if (_select._num == 0)   // ��������
            {
                _isWaiting = true;
                Invoke("Quit", 4.0f);
            }


            // ȿ����
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
