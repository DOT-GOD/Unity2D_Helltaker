using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStartManager : MonoBehaviour
{
    public GameObject _currentStage;
    private GameObject _currentPlayer;
    private bool _isWaiting = false;

    public GameObject[] _spawnPointGroup;

    public GameObject _cutAway;                             // 지정필요 : 장면전환 UI 오브젝트

    public GameObject[] _stageGroup;                        // 지정필요 : 레벨 오브젝트

    public GameObject _player;                              // 지정필요 : 플레이어 원본 오브젝트
    public GameObject _playerSpawnPoint;

    public GameObject _skeleton;                            // 지정필요 : 해골 원본 오브젝트
    public GameObject[] _monsterSpawnPointGroup;

    public GameObject[] _rock;                              // 지정필요 : 바위 원본 오브젝트
    public GameObject[] _rockSpawnPointGroup;

    public GameObject _thorn;                               // 지정필요 : 가시 원본 오브젝트
    public GameObject _upDownThorn;                         // 지정필요 : 오르내리는 가시 원본 오브젝트
    public GameObject[] _thornSpawnPointGroup;

    public GameObject _key;                                 // 지정필요 : 열쇠 원본 오브젝트
    public GameObject _keySpawnPoint;

    public GameObject _lock;                                // 지정필요 : 자물쇠 원본 오브젝트
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
        // 테스트용
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
            // 장면전환중이 아닐 때
            if (!_isWaiting)
            {
                // 플레이어가 승리시 다음레벨 전환
                if(_currentPlayer.GetComponent<Player>()._isWin)
                {
                    _isWaiting = true;

                    // 승리 이펙트
                    _currentPlayer.GetComponent<Player>().Win();

                    // 장면전환 화면
                    Invoke("CutAwayActive", 4.0f);
                    Invoke("NextLevel", 5.0f);
                }

                // 스테이지 재시작
                if(_currentStage.GetComponent<StageData>()._isRestarted)
                {
                    _currentStage.GetComponent<StageData>()._isRestarted = false;

                    // 장면전환 화면
                    CutAwayActive();
                    Invoke("RestartLevel", 1.0f);

                }

                // 사망시 지연 후 재시작
                if (_currentPlayer.GetComponent<Player>()._isDead)
                {
                    _currentPlayer.GetComponent<Player>()._isDead = false;

                    // 장면전환 화면
                    Invoke("CutAwayActive", 1.0f);
                    Invoke("RestartLevel", 2.0f);

                }


                // 스테이지 종료
                if (_currentStage.GetComponent<StageData>()._isExit)
                {
                    _currentStage.GetComponent<StageData>()._isExit = false;
                    // 장면전환 화면
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

    // 코루틴 최적화 문제로 사용안함(렉유발)
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
        Debug.Log("스테이지 종료");
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
        //원하는 스테이지 활성화
        for(int i = 0; i < _stageGroup.Length;i++)
        {
            _stageGroup[i].SetActive(false);
        }

        _currentStage = _stageGroup[num];
        _currentStage.SetActive(true);
        _currentStage.GetComponent<StageData>()._isRestarted = false;
        _currentStage.GetComponent<StageData>()._isPaused = false;

        // 스테이지8 위치 초기화
        _currentStage.transform.localPosition = new Vector2(0, 0);

        // 스폰포인트 태그 이용 방식
        //_playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        //_monsterSpawnPointGroup = GameObject.FindGameObjectsWithTag("MonsterSpawnPoint");
        //_rockSpawnPointGroup = GameObject.FindGameObjectsWithTag("RockSpawnPoint");
        //_thornSpawnPointGroup = GameObject.FindGameObjectsWithTag("ThornSpawnPoint");
        //_keySpawnPoint = GameObject.FindGameObjectWithTag("KeySpawnPoint");
        //_lockSpawnPoint = GameObject.FindGameObjectWithTag("LockSpawnPoint");

        // 스폰포인트 데이터 이용방식
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
        //이미 생성된 플레이어 제거
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
        //이미 생성된 몬스터 제거
        GameObject[] _monsterSpawned = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < _monsterSpawned.Length; i++)
        {
            Destroy(_monsterSpawned[i]);
        }

        //몬스터 스폰
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
        //이미 생성된 바위 제거
        GameObject[] _rockSpawned = GameObject.FindGameObjectsWithTag("Rock");

        for (int i = 0; i < _rockSpawned.Length; i++)
        {
            Destroy(_rockSpawned[i]);
        }

        //바위 스폰
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
        //이미 생성된 가시 제거
        GameObject[] _thornSpawned = GameObject.FindGameObjectsWithTag("Thorn");

        for (int i = 0; i < _thornSpawned.Length; i++)
        {
            Destroy(_thornSpawned[i]);
        }

        //가시 스폰
        for (int i = 0; i < _thornSpawnPointGroup.Length; i++)
        {
            GameObject tempthorn;
            // 5스테이지부터는 움직이는 가시
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
        //이미 생성된 키 제거
        GameObject[] _keySpawned = GameObject.FindGameObjectsWithTag("Key");

        for (int i = 0; i < _keySpawned.Length; i++)
        {
            Destroy(_keySpawned[i]);
        }

        //열쇠 스폰
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
        //이미 생성된 자물쇠 제거
        GameObject[] _lockSpawned = GameObject.FindGameObjectsWithTag("Lock");

        for (int i = 0; i < _lockSpawned.Length; i++)
        {
            Destroy(_lockSpawned[i]);
        }

        //자물쇠 스폰
        if(_lockSpawnPoint != null)
        {
            GameObject tempLock;
            tempLock = GameObject.Instantiate(_lock, _lockSpawnPoint.transform.position, Quaternion.identity);
            tempLock.transform.rotation = _lockSpawnPoint.transform.rotation;
            tempLock.SetActive(true);
        }
    }
}
