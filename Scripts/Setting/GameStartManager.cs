using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStartManager : MonoBehaviour
{
    public GameObject _currentStage;
    private GameObject _currentPlayer;
    private bool _isWaiting = false;

    public GameObject[] _spawnPointGroup;

    public GameObject _cutAway;                             // �����ʿ� : �����ȯ UI ������Ʈ

    public GameObject[] _stageGroup;                        // �����ʿ� : ���� ������Ʈ

    public GameObject _player;                              // �����ʿ� : �÷��̾� ���� ������Ʈ
    public GameObject _playerSpawnPoint;

    public GameObject _skeleton;                            // �����ʿ� : �ذ� ���� ������Ʈ
    public GameObject[] _monsterSpawnPointGroup;

    public GameObject[] _rock;                              // �����ʿ� : ���� ���� ������Ʈ
    public GameObject[] _rockSpawnPointGroup;

    public GameObject _thorn;                               // �����ʿ� : ���� ���� ������Ʈ
    public GameObject _upDownThorn;                         // �����ʿ� : ���������� ���� ���� ������Ʈ
    public GameObject[] _thornSpawnPointGroup;

    public GameObject _key;                                 // �����ʿ� : ���� ���� ������Ʈ
    public GameObject _keySpawnPoint;

    public GameObject _lock;                                // �����ʿ� : �ڹ��� ���� ������Ʈ
    public GameObject _lockSpawnPoint;

    void Start()
    {
        for (int i = 0; i < _stageGroup.Length; i++)
        {
            _stageGroup[i].SetActive(false);
        }
    }

    void Update()
    {
        // �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LevelSetup(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            LevelSetup(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            LevelSetup(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            LevelSetup(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            LevelSetup(5);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            LevelSetup(6);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            LevelSetup(7);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            LevelSetup(8);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            LevelSetup(9);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            ExitLevel();

            if (_currentPlayer != null)
        {
            // �����ȯ���� �ƴ� ��
            if (!_isWaiting)
            {
                // �÷��̾ �¸��� �������� ��ȯ
                if(_currentPlayer.GetComponent<Player>()._isWin)
                {
                    _isWaiting = true;

                    // �¸� ����Ʈ
                    _currentPlayer.GetComponent<Player>().Win();

                    // �����ȯ ȭ��
                    Invoke("CutAwayActive", 4.0f);
                    Invoke("NextLevel", 5.0f);
                }

                // �������� �����
                if(_currentStage.GetComponent<StageData>()._isRestarted)
                {
                    _currentStage.GetComponent<StageData>()._isRestarted = false;

                    // �����ȯ ȭ��
                    CutAwayActive();
                    Invoke("RestartLevel", 1.0f);

                }

                // ����� ���� �� �����
                if (_currentPlayer.GetComponent<Player>()._isDead)
                {
                    _currentPlayer.GetComponent<Player>()._isDead = false;

                    // �����ȯ ȭ��
                    Invoke("CutAwayActive", 1.0f);
                    Invoke("RestartLevel", 2.0f);

                }


                // �������� ����
                if (_currentStage.GetComponent<StageData>()._isExit)
                {
                    _currentStage.GetComponent<StageData>()._isExit = false;
                    // �����ȯ ȭ��
                    CutAwayActive();
                    Invoke("ExitLevel", 1.0f);

                }
            }
        }
    }
    private void CutAwayActive()
    {
        _cutAway.SetActive(true);
    }

    // �ڷ�ƾ ����ȭ ������ ������(������)
    /*
    IEnumerator Active(GameObject s, float waitNum)
    {
        yield return YieldCache.WaitForSeconds(waitNum);
        s.SetActive(true);
    }
    IEnumerator Inactive(GameObject s, float waitNum)
    {
        yield return YieldCache.WaitForSeconds(waitNum);
        s.SetActive(false);
    }
    IEnumerator NextLevel(float waitNum)
    {
        yield return YieldCache.WaitForSeconds(waitNum);

        int num = 0;
        for (int i = 0; i < _stageGroup.Length; i++)
        {
            if (_currentStage == _stageGroup[i])
                num = i;
        }

        LevelSetup(num + 1);

        _isWaiting = false;
    }
    IEnumerator RestartLevel(float waitNum)
    {
        yield return new WaitForSeconds(waitNum);

        int num = 0;
        for (int i = 0; i < _stageGroup.Length; i++)
        {
            if (_currentStage == _stageGroup[i])
                num = i;
        }

        LevelSetup(num);

        _currentStage.GetComponent<StageData>()._isRestarted = false;
        _isWaiting = false;
    }
    */

    private void ExitLevel()
    {
        Debug.Log("�������� ����");
        int num = 0;

        LevelSetup(num);

        _isWaiting = false;
    }

    private void NextLevel()
    {
        int num = 0;
        for (int i = 0; i < _stageGroup.Length; i++)
        {
            if (_currentStage == _stageGroup[i])
                num = i;
        }

        LevelSetup(num + 1);

        _isWaiting = false;
    }

    private void RestartLevel()
    {
        int num = 0;
        for (int i = 0; i < _stageGroup.Length; i++)
        {
            if (_currentStage == _stageGroup[i])
                num = i;
        }

        LevelSetup(num);

        _isWaiting = false;
    }

    public void LevelSetup(int num)
    {
        if(num < _stageGroup.Length)
        {
            StageSetUp(num);
            PlayerRespawn();
            MonsterRespawn();
            RockRespawn();
            ThornRespawn();
            KeyRespawn();
            LockRespawn();
        }
    }

    private void StageSetUp(int num)
    {
        //���ϴ� �������� Ȱ��ȭ
        for(int i = 0; i < _stageGroup.Length;i++)
        {
            _stageGroup[i].SetActive(false);
        }

        _currentStage = _stageGroup[num];
        _currentStage.SetActive(true);
        _currentStage.GetComponent<StageData>()._isRestarted = false;
        _currentStage.GetComponent<StageData>()._isPaused = false;

        // ��������8 ��ġ �ʱ�ȭ
        _currentStage.transform.localPosition = new Vector2(0, 0);

        // ��������Ʈ �±� �̿� ���
        //_playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        //_monsterSpawnPointGroup = GameObject.FindGameObjectsWithTag("MonsterSpawnPoint");
        //_rockSpawnPointGroup = GameObject.FindGameObjectsWithTag("RockSpawnPoint");
        //_thornSpawnPointGroup = GameObject.FindGameObjectsWithTag("ThornSpawnPoint");
        //_keySpawnPoint = GameObject.FindGameObjectWithTag("KeySpawnPoint");
        //_lockSpawnPoint = GameObject.FindGameObjectWithTag("LockSpawnPoint");

        // ��������Ʈ ������ �̿���
        _spawnPointGroup = GameObject.FindGameObjectsWithTag("SpawnPoint");

        _playerSpawnPoint = null;
        _keySpawnPoint = null;
        _lockSpawnPoint = null;
        _monsterSpawnPointGroup = null;
        _rockSpawnPointGroup = null;
        _thornSpawnPointGroup = null;

        List<GameObject> _listMonster = new List<GameObject>();
        List<GameObject> _listRock = new List<GameObject>();
        List<GameObject> _listThorn = new List<GameObject>();

        for (int i = 0; i < _spawnPointGroup.Length;i++)
        {
            SpawnPointType _tempType = _spawnPointGroup[i].GetComponent<SpawnPointData>()._type;

            if (_tempType == SpawnPointType.Player)
                _playerSpawnPoint = _spawnPointGroup[i];
            else if (_tempType == SpawnPointType.Monster)
                _listMonster.Add(_spawnPointGroup[i]);
            else if (_tempType == SpawnPointType.Rock)
                _listRock.Add(_spawnPointGroup[i]);
            else if (_tempType == SpawnPointType.Thorn)
                _listThorn.Add(_spawnPointGroup[i]);
            else if (_tempType == SpawnPointType.Key)
                _keySpawnPoint = _spawnPointGroup[i];
            else if (_tempType == SpawnPointType.Lock)
                _lockSpawnPoint = _spawnPointGroup[i];
        }
        _monsterSpawnPointGroup = _listMonster.ToArray();
        _rockSpawnPointGroup = _listRock.ToArray();
        _thornSpawnPointGroup = _listThorn.ToArray();


    }
    private void PlayerRespawn()
    {
        //�̹� ������ �÷��̾� ����
        GameObject[] _playerSpawned = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < _playerSpawned.Length; i++)
        {
            Destroy(_playerSpawned[i]);
        }

        GameObject tempPlayer;
        if(_playerSpawnPoint != null)
        {
            tempPlayer = GameObject.Instantiate(_player, _playerSpawnPoint.transform.position, Quaternion.identity);
            tempPlayer.transform.rotation = _playerSpawnPoint.transform.rotation;
            tempPlayer.SetActive(true);

            _currentPlayer = tempPlayer;
        }

    }

    private void MonsterRespawn()
    {
        //�̹� ������ ���� ����
        GameObject[] _monsterSpawned = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < _monsterSpawned.Length; i++)
        {
            Destroy(_monsterSpawned[i]);
        }

        //���� ����
        for (int i = 0; i < _monsterSpawnPointGroup.Length; i++)
        {
            GameObject tempMonster;
            tempMonster = GameObject.Instantiate(_skeleton, _monsterSpawnPointGroup[i].transform.position, Quaternion.identity);
            tempMonster.transform.rotation = _monsterSpawnPointGroup[i].transform.rotation;
            tempMonster.transform.SetParent(_currentStage.transform, true);
            tempMonster.SetActive(true);
        }
    }

    private void RockRespawn()
    {
        //�̹� ������ ���� ����
        GameObject[] _rockSpawned = GameObject.FindGameObjectsWithTag("Rock");

        for (int i = 0; i < _rockSpawned.Length; i++)
        {
            Destroy(_rockSpawned[i]);
        }

        //���� ����
        for (int i = 0; i < _rockSpawnPointGroup.Length; i++)
        {
            GameObject tempRock;
            tempRock = GameObject.Instantiate(_rock[i%_rock.Length], _rockSpawnPointGroup[i].transform.position, Quaternion.identity);
            tempRock.transform.rotation = _rockSpawnPointGroup[i].transform.rotation;
            tempRock.SetActive(true);
        }
    }

    private void ThornRespawn()
    {
        //�̹� ������ ���� ����
        GameObject[] _thornSpawned = GameObject.FindGameObjectsWithTag("Thorn");

        for (int i = 0; i < _thornSpawned.Length; i++)
        {
            Destroy(_thornSpawned[i]);
        }

        //���� ����
        for (int i = 0; i < _thornSpawnPointGroup.Length; i++)
        {
            GameObject tempthorn;
            // 5�����������ʹ� �����̴� ����
            if(_thornSpawnPointGroup[i].GetComponent<SpawnPointData>()._isUpDownThorn)
            {
                if (_thornSpawnPointGroup[i].GetComponent<SpawnPointData>()._isBurrowedThorn)
                    _upDownThorn.GetComponent<Thorn>()._isBurrowed = true;
                else
                    _upDownThorn.GetComponent<Thorn>()._isBurrowed = false;

                tempthorn = GameObject.Instantiate(_upDownThorn, _thornSpawnPointGroup[i].transform.position, Quaternion.identity);
            }
            else
                tempthorn = GameObject.Instantiate(_thorn, _thornSpawnPointGroup[i].transform.position, Quaternion.identity);

            tempthorn.transform.rotation = _thornSpawnPointGroup[i].transform.rotation;
            tempthorn.SetActive(true);
        }
    }

    private void KeyRespawn()
    {
        //�̹� ������ Ű ����
        GameObject[] _keySpawned = GameObject.FindGameObjectsWithTag("Key");

        for (int i = 0; i < _keySpawned.Length; i++)
        {
            Destroy(_keySpawned[i]);
        }

        //���� ����
        if(_keySpawnPoint != null)
        {
            GameObject tempKey;
            tempKey = GameObject.Instantiate(_key, _keySpawnPoint.transform.position, Quaternion.identity);
            tempKey.transform.rotation = _keySpawnPoint.transform.rotation;
            tempKey.SetActive(true);
        }
    }

    private void LockRespawn()
    {
        //�̹� ������ �ڹ��� ����
        GameObject[] _lockSpawned = GameObject.FindGameObjectsWithTag("Lock");

        for (int i = 0; i < _lockSpawned.Length; i++)
        {
            Destroy(_lockSpawned[i]);
        }

        //�ڹ��� ����
        if(_lockSpawnPoint != null)
        {
            GameObject tempLock;
            tempLock = GameObject.Instantiate(_lock, _lockSpawnPoint.transform.position, Quaternion.identity);
            tempLock.transform.rotation = _lockSpawnPoint.transform.rotation;
            tempLock.SetActive(true);
        }
    }
}
