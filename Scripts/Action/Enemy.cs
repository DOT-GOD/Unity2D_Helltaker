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

    public GameObject _deathParticle;              // �����ʿ� : ����� ȿ�� ������Ʈ

    //���� wasd 0~3
    public GameObject[] _checkerObject;            // �����ʿ� : �浹üũ�� �ڽ� ������Ʈ
    public ColliderChecker[] _checker;
    public bool[] _isBlocked;

    public GameObject _skeletonKickSFX;            // �����ʿ� : �ذ� ������ �Ҹ� ������Ʈ
    public GameObject _skeletonDieSFX;             // �����ʿ� : ��� �Ҹ� ������Ʈ

    public bool _isRock = false;
    public GameObject _rockKickSFX;                // �����ʿ� : ���� ������ �Ҹ� ������Ʈ

    public bool _isLock = false;
    public GameObject _lockKickSFX;                // �����ʿ� : �ڹ��� ������ �Ҹ� ������Ʈ

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
        //�����¿� üũ �ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            _checker[i] = _checkerObject[i].gameObject.GetComponent<ColliderChecker>();
        }

        // ���� �������� üũ
        _currentStage = GameObject.FindGameObjectWithTag("Stage");
    }

    void Update()
    {
        // �Ͻ���������
        if (_currentStage.GetComponent<StageData>()._isPaused)
            return;

        //�ð�������
        _currentTime += Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            _isBlocked[i] = _checker[i]._nearWall || _checker[i]._nearRock
                || _checker[i]._nearMonster || _checker[i]._nearLock || _checker[i]._nearGoal;
        }

        if(!_isDead)
        {
            //�˹鱸��
            //�뽬���϶��� �̵�
            if (_isDashing == true && _dashTime > _currentTime - _lastDashTime)
            {
                //��ǥ�������� �̵�
                this.transform.position = Vector2.MoveTowards(this.transform.position, _dir, Time.deltaTime * _dashSpeed);
                //Debug.Log("�̵���");
            }
            if (_isDashing == true && _dashTime < _currentTime - _lastDashTime)
            {
                _isDashing = false;
                //Debug.Log("����");
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

        //����
        if (_isRock)
        {
            GameObject _sound = new GameObject();
            SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
            _soundComponent.Play(_rockKickSFX);
        }

        if (_isBlocked[dirNum])
        {
            //���Ʈ����
            if (!_isRock && !_isLock)
            {
                Dead(dirNum);
            }
            return;
        }
        
        //�뽬 ����
        if(!_isLock)
        {
            // ������ �뽬 ������� ������ �����̺��� ���� �ð��� ������ ��
            if (_dashDelay < _currentTime - _lastDashTime)
            {
                _isDashing = true;
                _lastDashTime = _currentTime;   //������ ��� ����

                AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
                if (_aniCon != null)
                    _aniCon.Play(1);
            }
        }

        // ����
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

    //���÷� ���� ���
    public void Dead()
    {
        _isDead = true;
        AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
        _aniCon.Play(2);
        GameObject tempParticle;
        tempParticle = GameObject.Instantiate(_deathParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(tempParticle, 2.0f);
        tempParticle.SetActive(true);

        // ����
        GameObject _sound = new GameObject();
        SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
        _soundComponent.Play(_skeletonDieSFX);

        Destroy(this.gameObject);
    }
    // ������� ���� ���
    public void Dead(int dirNum)
    {
        _isDead = true;
        AnimatorController _aniCon = this.gameObject.GetComponent<AnimatorController>();
        _aniCon.Play(2);
        GameObject tempParticle;
        tempParticle = GameObject.Instantiate(_deathParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(tempParticle, 2.0f);
        tempParticle.SetActive(true);

        // ����
        GameObject _sound = new GameObject();
        SoundManager _soundComponent = _sound.AddComponent<SoundManager>();
        _soundComponent.Play(_skeletonDieSFX);

        //����� �÷��̾� ���� ����
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        Player _playerComponent = _player.gameObject.GetComponent<Player>();
        _playerComponent._nearObject[dirNum] = _playerComponent._dummy;
        _playerComponent._canAttack[dirNum] = false;

        Destroy(this.gameObject);
    }
}

