using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    public GameObject[] _menu;               // �����ʿ� : �� ������ �޴� ������Ʈ
    public GameObject _selectSFX;            // �����ʿ� : ���� ȿ����

    public int _num = 0;
    public bool _isPaused = false;



    void Start()
    {
        selectedButton();

    }

    void Update()
    {
        if(!_isPaused)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _num -= 1;
                if (_num < 0)
                    _num = _menu.Length -1;
                selectedButton();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _num += 1;
                if (_num >= _menu.Length)
                    _num = 0;
                selectedButton();
            }

        }
    }

    private void selectedButton()
    {
        GameObject _sound = new GameObject();
        SoundManager _soundManager = _sound.gameObject.AddComponent<SoundManager>();
        _soundManager.Play(_selectSFX);

        for (int i = 0; i < _menu.Length; i++)
        {
            _menu[i].GetComponent<Choice>()._isSelected = false;
        }
        _menu[_num].GetComponent<Choice>()._isSelected = true;
    }
}
