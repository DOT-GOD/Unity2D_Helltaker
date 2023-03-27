using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueProgress : MonoBehaviour
{
    public int _progress = 0;
    private int _maxProgress = 0;

    public bool _isWin = false;                            // 지정필요 : 승리장면 여부

    public GameObject _selectMenu;                          // 지정필요 : (존재할 경우)선택지
    public GameObject _nextScene;                           // 지정필요 : (존재할 경우)다음 장면

    public GameObject[] _BGSprites;                         // 지정필요 : 현재 장면에서 사용될 모든 배경 스프라이트
    public int[] _BGSpriteChanger;                          // 지정필요 : 지정한 진행상황에서 배경 스프라이트 오브젝트 변경

    public GameObject[] _standingSprites;                   // 지정필요 : 현재 장면에서 사용될 모든 캐릭터 스프라이트
    public int[] _standingSpriteChanger;                    // 지정필요 : 지정한 진행상황에서 캐릭터 스프라이트 오브젝트 변경

    public Text _nameText;                                  // 지정필요 : 이름을 표시할 텍스트 오브젝트
    public string[] _nameList;                              // 지정필요 : 사용될 이름 목록
    public int[] _nameChanger;                              // 지정필요 : 지정한 진행상황에서 캐릭터 이름 텍스트내용 변경
    public Text _dialogueText;                              // 지정필요 : 대화문을 표시할 텍스트 오브젝트

    public GameObject _booper;                              // 지정필요 : 대화 중 표시될 오브젝트
    public string[] _dialogueString;                        // 지정필요 : []번째 대화문에 표시될 내용

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
            // 다음 장면으로 전환
            if (_progress == _maxProgress)
            {
                // 선택지가 있는지 판정
                if (_selectMenu == null)
                {
                    // 힌트인 경우 다음장면 대신 정지해제
                    if (_isHint)
                    {
                        StageData _currentData = GameObject.FindGameObjectWithTag("Stage").GetComponent<StageData>();
                        if (_currentData != null)
                            _currentData._isPaused = false;

                        // 승리 장면인 경우
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
                        // 다음 장면이 스테이지인지 판정
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
                //선택지가 있는 경우
                else
                {
                    //_dialogueText.text = "";
                    _booper.SetActive(false);
                    _selectMenu.SetActive(true);
                    return;
                }
            }

            // 대화문 띄어쓰기 인식
            _dialogueText.text = _dialogueString[_progress].Replace("\\n", "\n");

            // 글자가 wait초 이후 타이핑되는 효과
            //StartCoroutine(TypeText(_dialogueString[_progress].Replace("\\n", "\n"), 0f));

            // _nameChanger[i] 순서에서 이름 텍스트 교체
            for (int i = 0; i < _nameChanger.Length; i++)
            {
                if (_progress == _nameChanger[i])
                {
                    _nameText.text = _nameList[i+1].Replace("\\n", "\n");
                }
            }
            // _BGSpriteChanger[i] 순서에서 배경 스프라이트 교체
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

            // _standingSpriteChanger[i] 순서에서 캐릭터 스프라이트 교체
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

    // 글자가 wait초 이후 타이핑되는 효과
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
        // 진행초기화
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

        // 글자가 wait초 이후 타이핑되는 효과
        //StartCoroutine(TypeText(_dialogueString[_progress].Replace("\\n", "\n"), 0f));

        // 최대 대화진행 = 대사 수
        _maxProgress = _dialogueString.Length;

        _gameStartManager = GameObject.FindGameObjectWithTag("GameStartManager");

        // 다음 장면, 선택지가 없다 = 현재 대화는 힌트다
        if (_nextScene == null && _selectMenu == null)
            _isHint = true;
        
        // 선택지 비활성화
        if (_selectMenu != null)
        {
            _selectMenu.SetActive(false);
        }

    }
}
