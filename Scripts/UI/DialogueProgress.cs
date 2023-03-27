using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueProgress : MonoBehaviour
{
    public int _progress = 0;
    private int _maxProgress = 0;

    public bool _isWin = false;                            // �����ʿ� : �¸���� ����

    public GameObject _selectMenu;                          // �����ʿ� : (������ ���)������
    public GameObject _nextScene;                           // �����ʿ� : (������ ���)���� ���

    public GameObject[] _BGSprites;                         // �����ʿ� : ���� ��鿡�� ���� ��� ��� ��������Ʈ
    public int[] _BGSpriteChanger;                          // �����ʿ� : ������ �����Ȳ���� ��� ��������Ʈ ������Ʈ ����

    public GameObject[] _standingSprites;                   // �����ʿ� : ���� ��鿡�� ���� ��� ĳ���� ��������Ʈ
    public int[] _standingSpriteChanger;                    // �����ʿ� : ������ �����Ȳ���� ĳ���� ��������Ʈ ������Ʈ ����

    public Text _nameText;                                  // �����ʿ� : �̸��� ǥ���� �ؽ�Ʈ ������Ʈ
    public string[] _nameList;                              // �����ʿ� : ���� �̸� ���
    public int[] _nameChanger;                              // �����ʿ� : ������ �����Ȳ���� ĳ���� �̸� �ؽ�Ʈ���� ����
    public Text _dialogueText;                              // �����ʿ� : ��ȭ���� ǥ���� �ؽ�Ʈ ������Ʈ

    public GameObject _booper;                              // �����ʿ� : ��ȭ �� ǥ�õ� ������Ʈ
    public string[] _dialogueString;                        // �����ʿ� : []��° ��ȭ���� ǥ�õ� ����

    private GameObject _gameStartManager;

    private bool _isHint = false;


    void Start()
    {
        Initialize();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if(_progress < _maxProgress)
                _progress += 1;
            // ���� ������� ��ȯ
            if (_progress == _maxProgress)
            {
                // �������� �ִ��� ����
                if (_selectMenu == null)
                {
                    // ��Ʈ�� ��� ������� ��� ��������
                    if (_isHint)
                    {
                        StageData _currentData = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageData>();
                        if (_currentData != null)
                            _currentData._isPaused = false;

                        // �¸� ����� ���
                        if (_isWin)
                        {
                            Player _tempPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
                            _tempPlayer._isWin = true;
                        }
                    }
                    else
                    {
                        GameStartManager _tempManager = _gameStartManager.GetComponent<GameStartManager>();
                        StageData _tempData = _nextScene.GetComponent<StageData>();
                        // ���� ����� ������������ ����
                        if (_tempData != null)
                        {
                            _tempManager.LevelSetup(_tempData._stageNum);
                        }
                        else
                            _nextScene.gameObject.SetActive(true);
                    }

                    this.gameObject.SetActive(false);
                    Initialize();
                    return;
                }
                //�������� �ִ� ���
                else
                {
                    //_dialogueText.text = "";
                    _booper.SetActive(false);
                    _selectMenu.SetActive(true);
                    return;
                }
            }

            // ��ȭ�� ���� �ν�
            _dialogueText.text = _dialogueString[_progress].Replace("\\n", "\n");

            // ���ڰ� wait�� ���� Ÿ���εǴ� ȿ��
            //StartCoroutine(TypeText(_dialogueString[_progress].Replace("\\n", "\n"), 0f));

            // _nameChanger[i] �������� �̸� �ؽ�Ʈ ��ü
            for (int i = 0; i < _nameChanger.Length; i++)
            {
                if (_progress == _nameChanger[i])
                {
                    _nameText.text = _nameList[i+1].Replace("\\n", "\n");
                }
            }
            // _BGSpriteChanger[i] �������� ��� ��������Ʈ ��ü
            for (int i = 0; i < _BGSpriteChanger.Length; i++)
            {
                if (_progress == _BGSpriteChanger[i])
                {
                    if (_BGSprites[i].gameObject != null)
                        _BGSprites[i].SetActive(false);
                    if (_BGSprites[i + 1].gameObject != null)
                        _BGSprites[i + 1].SetActive(true);
                }
            }

            // _standingSpriteChanger[i] �������� ĳ���� ��������Ʈ ��ü
            for (int i = 0; i < _standingSpriteChanger.Length; i++)
            {
                if (_progress == _standingSpriteChanger[i])
                {
                    if (_standingSprites[i].gameObject != null)
                        _standingSprites[i].SetActive(false);
                    if (_standingSprites[i + 1].gameObject != null)
                        _standingSprites[i + 1].SetActive(true);
                }
            }
            
        }


    }

    // ���ڰ� wait�� ���� Ÿ���εǴ� ȿ��
    IEnumerator TypeText(string narration,float wait)
    {
        yield return new WaitForSeconds(wait);

        string writerText = "";
        for(int i = 0; i< narration.Length;i++)
        {
            writerText += narration[i];
            _dialogueText.text = writerText;
            yield return null;
        }
    }

    public void Initialize()
    {
        // �����ʱ�ȭ
        _progress = 0;

        _booper.SetActive(true);

        if (_BGSprites[0].gameObject != null)
        {
            for (int i = 0; i < _BGSprites.Length; i++)
            {
                if (_BGSprites[i] != null)
                    _BGSprites[i].SetActive(false);
            }
            _BGSprites[0].SetActive(true);

        }

        if (_standingSprites[0].gameObject != null)
        {
            for (int i = 0; i < _standingSprites.Length; i++)
            {
                if (_standingSprites[i] != null)
                    _standingSprites[i].SetActive(false);
            }
            _standingSprites[0].SetActive(true);
        }

        _nameText.text = _nameList[0].Replace("\\n", "\n");
        _dialogueText.text = _dialogueString[0].Replace("\\n", "\n");

        // ���ڰ� wait�� ���� Ÿ���εǴ� ȿ��
        //StartCoroutine(TypeText(_dialogueString[_progress].Replace("\\n", "\n"), 0f));

        // �ִ� ��ȭ���� = ��� ��
        _maxProgress = _dialogueString.Length;

        _gameStartManager = GameObject.FindGameObjectWithTag("GameStartManager");

        // ���� ���, �������� ���� = ���� ��ȭ�� ��Ʈ��
        if (_nextScene == null && _selectMenu == null)
            _isHint = true;
        
        // ������ ��Ȱ��ȭ
        if (_selectMenu != null)
        {
            _selectMenu.SetActive(false);
        }

    }
}
