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

    public Text _scoreBoard;                    // �����ʿ� : �÷��̾� ���� ü�� ǥ�ÿ� �ؽ�Ʈ
    public Text _stageBoard;                    // �����ʿ� : ���� �������� ��ȣ ǥ�ÿ� �ؽ�Ʈ
    public GameObject _UI;                      // �����ʿ� : �������� ���� UI �ڽĿ�����Ʈ
    private GameObject _player;
    private Player _playerComponent;

    public bool _isPaused = false;
    public bool _isRestarted = false;
    public bool _isExit = false;

    public GameObject _hintDialogue;            // �����ʿ� : ��Ʈ ��ȭ ������Ʈ
    public GameObject _loveDialogue;            // �����ʿ� : ���� ��ȭ ������Ʈ

    void Start()
    {
        // ���� �������� ǥ��
        StageBoard();
    }

    void Update()
    {
        // ������ uiȰ��ȭ ����
        if (_isPaused)
            _UI.SetActive(false);
        else
            _UI.SetActive(true);


        // �Ͻ�����
        if (_isPaused)
            return;

        // �÷��̾� ���� ü�� ǥ��
        // �÷��̾ ������ �� �� �����Ƿ� ������Ʈ���� ó��
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            _playerComponent = _player.GetComponent<Player>();

            if (_playerComponent._playerHp > 0)
                _scoreBoard.text = _playerComponent._playerHp.ToString();
            else
                _scoreBoard.text = "X";
        }

        // �λ�����(��Ʈ����) ��ư
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (_hintDialogue != null)
            {
                _hintDialogue.SetActive(true);
                _isPaused = true;
            }
        }

        // ����� ��ư
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
