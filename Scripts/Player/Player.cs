using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    [Range(1.0f, 20.0f)]
    public float _dashSpeed = 5.0f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    public float _dashDelay = 0.3f;

    [SerializeField]
    [Range(0, 50)]
    public int _playerHp = 50;

    public GameObject _spriteObject;          // 지정필요 : 플레이어 스프라이트
    public AudioSource _footstep;             // 지정필요 : 발소리 오디오
    public GameObject _dummy;                 // 지정필요 : 더미 오브젝트
    public GameObject _deathSprite;           // 지정필요 : 플레이어 사망 스프라이트

    public GameObject _bloodEffect;           // 지정필요 : 피격 효과
    public GameObject _bloodSFX;              // 지정필요 : 피격 효과음

    public GameObject[] _attackEffect;          // 지정필요 : 타격 효과

    //방향 wasd 0~3
    public GameObject[] _checkerObject;       // 지정필요 : 충돌체크용 자식 오브젝트
    public ColliderChecker[] _checker;
    public GameObject[] _nearObject;
    public bool[] _canAttack;
    public bool _haveKey = false;
    public bool _isArrived = false;
    public bool _isWin = false;
    private bool[] _isBlocked;

    public bool _isDead = false;

    private float _currentTime = 0.0f;
    public bool _isDashing = false;
    private float _lastDashTime = 0.0f;
    private float _dashTime = 0.3f;

    private Vector2 _dir = new Vector2(0, 0);
    private Vector2 _dirLv8 = new Vector2(0, 0);

    private SpriteRenderer[] _renderers = null;

    private GameObject _currentStage;

    public GameObject[] _thorns;

    public bool _isOnThorn = false;
    private bool _thornAlreadyAdded = false;

    void Start()
    {
        _isBlocked = new bool[4];
        for(int i = 0; i < 4;i++)
        {
            _isBlocked[i] = false;
        }
        _canAttack = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            _canAttack[i] = false;
        }

        // 상하좌우 체크 초기화
        for (int i = 0; i < 4; i++)
        {
            _checker[i] = _checkerObject[i].gameObject.GetComponent<ColliderChecker>();
        }

        // 발소리 초기화
        _footstep = this.gameObject.GetComponent<AudioSource>();

        _renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();

        // 현재 스테이지 체크
        _currentStage = GameObject.FindGameObjectWithTag("Stage");

        // 체력 초기화
        _playerHp = _currentStage.GetComponent<StageData>()._stageHp;
    }

    void Update()
    {
        if(!_thornAlreadyAdded)
            ThornAdd();

        // 일시정지상태
        if (_currentStage.GetComponent<StageData>()._isPaused)
            return;

        // 사망상태
        if (_isDead)
            return;

        // 레벨 클리어판정
        if (!_isArrived)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_checker[i]._nearGoal)
                {
                    _isArrived = true;

                    _currentStage.GetComponent<StageData>()._isPaused = true;
                    _currentStage.GetComponent<StageData>()._loveDialogue.SetActive(true);
                }
            }

            // 사망
            if(_playerHp < 1 && !_isDashing)
            {
                _currentStage.GetComponent<StageData>()._isPaused = true;
                _isDead = true;
                _spriteObject.SetActive(false);
                _deathSprite.SetActive(true);
            }
        }

        // 시간경과기록
        _currentTime += Time.deltaTime;

        // 막힌 방향 판정
        for (int i = 0; i < 4; i++)
        {
            _isBlocked[i] = _checker[i]._nearWall || _checker[i]._nearGoal;
        }


        // 가시 위에 있는지 판정
        _isOnThorn = false;
        for (int i = 0; i < _thorns.Length; i++)
        {
            if (_thorns[i].GetComponent<Thorn>()._playerOnThis)
                _isOnThorn = true;
        }


#if UNITY_ANDROID

#elif UNITY_EDITOR || UNITY_STANDALONE

        // 이동 및 공격 판정
        if (!_isDashing)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!_canAttack[0])
                {
                    if(!_isBlocked[0])
                        Move(0);
                }
                else
                    Attack(0);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!_canAttack[1])
                {
                    if (!_isBlocked[1])
                        Move(1);
                }
                else
                    Attack(1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {

                if (!_canAttack[2])
                {
                    if (!_isBlocked[2])
                        Move(2);
                }
                else
                    Attack(2);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (!_canAttack[3])
                {
                    if (!_isBlocked[3])
                        Move(3);
                }
                else
                    Attack(3);
            }
        }
#endif

        // 스테이지 8(루시퍼)에서는 y축 이동시 플레이어 대신 맵이 움직임
        if (_currentStage.GetComponent<StageData>()._stageNum == 8)
        {
            if(this.transform.position.y == _dir.y)
            {
                Dash();
            }
            else
            {
                //대쉬중일때만 이동
                if (_isDashing == true && _dashTime > _currentTime - _lastDashTime)
                {
                    ////목표지점으로 이동
                    _currentStage.transform.localPosition
                        = Vector2.MoveTowards(_currentStage.transform.localPosition, _dirLv8, Time.deltaTime * _dashSpeed);
                    //Debug.Log("이동중");
                }
            }
        }
        else
        {
            Dash();
        }

        if (_isDashing == true && _dashTime < _currentTime - _lastDashTime)
        {
            _isDashing = false;
            //Debug.Log("멈춤");
            if (_isOnThorn)
                Damaged(0.1f);
        }
    }

    private void Dash()
    {
        //대쉬중일때만 이동
        if (_isDashing == true && _dashTime > _currentTime - _lastDashTime)
        {
            //목표지점으로 이동
            this.transform.position = Vector2.MoveTowards(this.transform.position, _dir, Time.deltaTime * _dashSpeed);
            //Debug.Log("이동중");
        }
    }

    private void Move(int dirNum)
    {
        //Debug.Log("키입력");
        float x = 0;
        float y = 0;

        if (dirNum == 0)
            y = 1;
        else if (dirNum == 1)
        {
            _spriteObject.transform.eulerAngles = new Vector3(0, 180, 0);
            x = -1;
        }
        else if (dirNum == 2)
            y = -1;
        else if (dirNum == 3)
        {
            _spriteObject.transform.eulerAngles = new Vector3(0, 0, 0);
            x = 1;
        }

        Vector2 _tempDir = new Vector2(x,y);

        // 스테이지 8(루시퍼)에서는 y축 이동시 플레이어 대신 맵이 움직임
        _dirLv8 = new Vector2(0, _currentStage.transform.localPosition.y - _tempDir.y);

        _dir = new Vector2(this.transform.position.x + _tempDir.x, this.transform.position.y + _tempDir.y);


        // 마지막 대쉬 사용했을 때부터 딜레이보다 많은 시간이 지났을 때
        if (_dashDelay < _currentTime - _lastDashTime)
        {
            _footstep.Play();
            _isDashing = true;
            _lastDashTime = _currentTime;   //마지막 사용 갱신

            AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
            _aniCon.Play(3);
        }

        Damaged();
        ThornBurrow();
    }

    private void Attack(int dirNum)
    {
        if(dirNum == 1)
            _spriteObject.transform.eulerAngles = new Vector3(0, 180, 0);
        else if(dirNum == 3)
            _spriteObject.transform.eulerAngles = new Vector3(0, 0, 0);


        Damaged();

        Debug.Log("공격");
        _isDashing = true;
        _lastDashTime = _currentTime;   //마지막 사용 갱신

        // 공격시 이동하는 버그방지용
        _dir = new Vector2(this.transform.position.x, this.transform.position.y);
        _dirLv8 = new Vector2(_currentStage.transform.localPosition.x, _currentStage.transform.localPosition.y);

        AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
        _aniCon.Play(1);

        int _attackNum = Random.Range(0,_attackEffect.Length);
        GameObject _tempEffect = GameObject.Instantiate(_attackEffect[_attackNum], _nearObject[dirNum].transform.position, Quaternion.identity);
        _tempEffect.SetActive(true);
        Destroy(_tempEffect, 0.2f);

        Enemy _enemy = _nearObject[dirNum].GetComponent<Enemy>();
        _enemy.Damaged(dirNum);

        ThornBurrow();

    }

    private void Damaged()
    {
        // 입력된 수 중 가장 큰 수 반환
        _playerHp = Mathf.Max(_playerHp - 1, 0);
        //Debug.Log("잔여체력 : " + _playerHp);
    }
    public void Damaged(float num)
    {
        // 입력된 수 중 가장 큰 수 반환
        _playerHp = Mathf.Max(_playerHp - 1, 0);
        //Debug.Log("잔여체력 : " + _playerHp);

        // 피격시 깜박거리는 효과
        foreach (SpriteRenderer render in _renderers)
        {
            if (render != null)
                render.material.color = new Color(4, 0, 0);

            Invoke("ResetColor", num);
        }

        // 이펙트
        GameObject tempEffect;
        tempEffect = GameObject.Instantiate(_bloodEffect, this.transform.position, Quaternion.identity);
        tempEffect.transform.rotation = this.transform.rotation;
        tempEffect.SetActive(true);
        Destroy(tempEffect, 0.2f);

        // 사운드
        GameObject _sound = new GameObject();
        SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
        _soundComponent.Play(_bloodSFX);
    }

    private void ResetColor()
    {
        foreach (SpriteRenderer render in _renderers)
        {
            if(render != null)
                render.material.color = Color.white;
        }
    }

    // 게임스타트매니저에서 처리
    public void Win()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_checker[i]._nearGoal)
            {
                Goal tempGoal = _nearObject[i].GetComponent<Goal>();
                tempGoal.Win();

                AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
                _aniCon.Play(2);
            }
        }
    }

    private void ThornAdd()
    {
        _thorns = GameObject.FindGameObjectsWithTag("Thorn");
        _thornAlreadyAdded = true;
    }

    private void ThornBurrow()
    {
        for(int i = 0; i<_thorns.Length;i++)
        {
            if(_thorns[i].GetComponentInChildren<UpDownThorn>() != null)
            {
                Thorn _tempThornComponent = _thorns[i].GetComponent<Thorn>();
                _tempThornComponent._isBurrowed = !_tempThornComponent._isBurrowed;
            }
        }
    }

}
