using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 20.0f)]
    public float _dashSpeed = 5.0f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    public float _dashDelay = 0.3f;

    public GameObject _deathParticle;              // 지정필요 : 사망시 효과 오브젝트

    //방향 wasd 0~3
    public GameObject[] _checkerObject;            // 지정필요 : 충돌체크용 자식 오브젝트
    public ColliderChecker[] _checker;
    public bool[] _isBlocked;

    public GameObject _skeletonKickSFX;            // 지정필요 : 해골 발차기 소리 오브젝트
    public GameObject _skeletonDieSFX;             // 지정필요 : 사망 소리 오브젝트

    public bool _isRock = false;
    public GameObject _rockKickSFX;                // 지정필요 : 바위 발차기 소리 오브젝트

    public bool _isLock = false;
    public GameObject _lockKickSFX;                // 지정필요 : 자물쇠 발차기 소리 오브젝트

    public bool _isDead = false;
    private float _currentTime = 0.0f;
    private bool _isDashing = false;
    private float _lastDashTime = 0.0f;
    private float _dashTime = 0.3f;

    private Vector2 _dir = new Vector2(0, 0);

    private GameObject _currentStage;

    void Start()
    {
        _isBlocked = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            _isBlocked[i] = false;
        }
        //상하좌우 체크 초기화
        for (int i = 0; i < 4; i++)
        {
            _checker[i] = _checkerObject[i].gameObject.GetComponent<ColliderChecker>();
        }

        // 현재 스테이지 체크
        _currentStage = GameObject.FindGameObjectWithTag("Stage");
    }

    void Update()
    {
        // 일시정지상태
        if (_currentStage.GetComponent<StageData>()._isPaused)
            return;

        //시간경과기록
        _currentTime += Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            _isBlocked[i] = _checker[i]._nearWall || _checker[i]._nearRock
                || _checker[i]._nearMonster || _checker[i]._nearLock || _checker[i]._nearGoal;
        }

        if(!_isDead)
        {
            //넉백구현
            //대쉬중일때만 이동
            if (_isDashing == true && _dashTime > _currentTime - _lastDashTime)
            {
                //목표지점으로 이동
                this.transform.position = Vector2.MoveTowards(this.transform.position, _dir, Time.deltaTime * _dashSpeed);
                //Debug.Log("이동중");
            }
            if (_isDashing == true && _dashTime < _currentTime - _lastDashTime)
            {
                _isDashing = false;
                //Debug.Log("멈춤");
            }
        }
    }

    public void Damaged(int dirNum)
    {
        float x = 0;
        float y = 0;

        if (dirNum == 0)
            y = 1;
        else if (dirNum == 1)
            x = -1;
        else if (dirNum == 2)
            y = -1;
        else if (dirNum == 3)
            x = 1;

        Vector2 _tempDir = new Vector2(x, y);

        _dir = new Vector2(this.transform.position.x + _tempDir.x, this.transform.position.y + _tempDir.y);

        //사운드
        if (_isRock)
        {
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_rockKickSFX);
        }

        if (_isBlocked[dirNum])
        {
            //사망트리거
            if (!_isRock && !_isLock)
            {
                Dead(dirNum);
            }
            return;
        }
        
        //대쉬 판정
        if(!_isLock)
        {
            // 마지막 대쉬 사용했을 때부터 딜레이보다 많은 시간이 지났을 때
            if (_dashDelay < _currentTime - _lastDashTime)
            {
                _isDashing = true;
                _lastDashTime = _currentTime;   //마지막 사용 갱신

                AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
                if (_aniCon != null)
                    _aniCon.Play(1);
            }
        }

        // 사운드
        if (_isLock)
        {
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_lockKickSFX);
        }
        if (!_isRock && !_isLock)
        {
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_skeletonKickSFX);
        }
    }

    //가시로 인한 사망
    public void Dead()
    {
        _isDead = true;
        AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
        _aniCon.Play(2);
        GameObject tempParticle;
        tempParticle = GameObject.Instantiate(_deathParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(tempParticle, 2.0f);
        tempParticle.SetActive(true);

        // 사운드
        GameObject _sound = new GameObject();
        SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
        _soundComponent.Play(_skeletonDieSFX);

        Destroy(this.gameObject);
    }
    // 발차기로 인한 사망
    public void Dead(int dirNum)
    {
        _isDead = true;
        AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
        _aniCon.Play(2);
        GameObject tempParticle;
        tempParticle = GameObject.Instantiate(_deathParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(tempParticle, 2.0f);
        tempParticle.SetActive(true);

        // 사운드
        GameObject _sound = new GameObject();
        SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
        _soundComponent.Play(_skeletonDieSFX);

        //사망시 플레이어 버그 방지
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        Player _playerComponent = _player.gameObject.GetComponent<Player>();
        _playerComponent._nearObject[dirNum] = _playerComponent._dummy;
        _playerComponent._canAttack[dirNum] = false;

        Destroy(this.gameObject);
    }
}

