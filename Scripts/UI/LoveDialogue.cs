using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveDialogue : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    public int _correctChoice = 0;                  // �����ʿ� : �ùٸ� ������

    public GameObject _winScene;                    // �����ʿ� : �¸� ��� ������Ʈ
    public GameObject _deadScene;                   // �����ʿ� : ��� ��� ������Ʈ
    public GameObject _loveDialogue;                // �����ʿ� : ����ȭ�� ������Ʈ
    public GameObject _selectSFX;                   // �����ʿ� : ���� ȿ����

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
            // �ùٸ� ������
            if (_select._num == _correctChoice)
            {
                _winScene.SetActive(true);
                _loveDialogue.gameObject.SetActive(false);
            }
            // Ʋ�� ������
            else
            {
                _deadScene.SetActive(true);
                _loveDialogue.gameObject.SetActive(false);
            }

            // ȿ����
            GameObject _sound = new GameObject();
            SoundManager _soundManager = _sound.gameObject.AddComponent<SoundManager>();
            _soundManager.Play(_selectSFX);
        }
    }
}
