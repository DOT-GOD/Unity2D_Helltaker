using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageData : MonoBehaviour
{
    [SerializeField]
    [Range(0, 50)]
    public int _stageHp = 50;

    public int _stageNum = 0;

    public Text _scoreBoard;                    // 지정필요 : 플레이어 현재 체력 표시용 텍스트
    public Text _stageBoard;                    // 지정필요 : 현재 스테이지 번호 표시용 텍스트
    public GameObject _UI;                      // 지정필요 : 스테이지 개별 UI 자식오브젝트
    private GameObject _player;
    private Player _playerComponent;

    public bool _isPaused = false;
    public bool _isRestarted = false;
    public bool _isExit = false;

    public GameObject _hintDialogue;            // 지정필요 : 힌트 대화 오브젝트
    public GameObject _loveDialogue;            // 지정필요 : 연애 대화 오브젝트

    void Start()
    {
        // 현재 스테이지 표시
        StageBoard();
    }

    void Update()
    {
        // 정지시 ui활성화 여부
        if (_isPaused)
            _UI.SetActive(false);
        else
            _UI.SetActive(true);


        // 일시정지
        if (_isPaused)
            return;

        // 플레이어 현재 체력 표시
        // 플레이어가 리스폰 될 수 있으므로 업데이트에서 처리
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerComponent = _player.GetComponent<Player>();

            if (_playerComponent._playerHp > 0)
                _scoreBoard.text = _playerComponent._playerHp.ToString();
            else
                _scoreBoard.text = "X";
        }

        // 인생조언(힌트보기) 버튼
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (_hintDialogue != null)
            {
                _hintDialogue.SetActive(true);
                _isPaused = true;
            }
        }

        // 재시작 버튼
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isPaused = true;
            _isRestarted = true;
        }

    }

    private void StageBoard()
    {
        switch (_stageNum)
        {
            case 1:
                _stageBoard.text = "I";
                break;
            case 2:
                _stageBoard.text = "II";
                break;
            case 3:
                _stageBoard.text = "III";
                break;
            case 4:
                _stageBoard.text = "IV";
                break;
            case 5:
                _stageBoard.text = "V";
                break;
            case 6:
                _stageBoard.text = "VI";
                break;
            case 7:
                _stageBoard.text = "VII";
                break;
            case 8:
                _stageBoard.text = "VIII";
                break;
            case 9:
                _stageBoard.text = "IX";
                break;
            default:break;
        }
    }
}
