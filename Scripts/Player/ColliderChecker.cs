using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    [SerializeField]
    [Range(0,3)]
    public int _dir = 0;

    public bool _isPlayer = false;
    public bool _nearWall = false;
    public bool _nearMonster = false;
    public bool _nearGoal = false;
    public bool _nearRock = false;
    public bool _nearLock = false;

    public GameObject _player = null;
    public Player _playerComponent = null;


    void Start()
    {

    }

    void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        _player = GameObject.FindWithTag("Player");
        _playerComponent = _player.gameObject.GetComponent<Player>();
        //Debug.Log("¡¢√À");
        if (other.tag == "Wall" || other.tag == "Monster" || other.tag == "Goal" || other.tag == "Rock" || other.tag == "Lock")
        {
            if (_isPlayer && _playerComponent._nearObject[_dir] == _playerComponent._dummy)
            {
                _playerComponent._nearObject[_dir] = other.gameObject;
            }
        }

        if (other.tag == "Wall")
        {
            _nearWall = true;
        }
        if (other.tag == "Monster")
        {
            if(this.transform.parent.transform.parent.tag == "Player")
                _playerComponent._canAttack[_dir] = true;

            _nearMonster = true;
        }
        if (other.tag == "Goal")
        {
            _nearGoal = true;
        }
        if (other.tag == "Rock")
        {
            if (this.transform.parent.transform.parent.tag == "Player")
                _playerComponent._canAttack[_dir] = true;
            _nearRock = true;
        }
        if (other.tag == "Lock")
        {
            if (!_playerComponent._haveKey)
            {
                if (this.transform.parent.transform.parent.tag == "Player")
                    _playerComponent._canAttack[_dir] = true;
            }
            else
            {
                if (this.transform.parent.transform.parent.tag == "Player")
                    _playerComponent._canAttack[_dir] = false;
            }
            _nearLock = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _player = GameObject.FindWithTag("Player");
        _playerComponent = _player.gameObject.GetComponent<Player>();
        if (other.tag == "Wall" || other.tag == "Monster" || other.tag == "Goal" || other.tag == "Rock" || other.tag == "Lock")
        {
            _playerComponent._nearObject[_dir] = _playerComponent._dummy;
        }
        if (other.tag == "Wall")
        {
            _nearWall = false;
        }
        if (other.tag == "Monster")
        {
            _playerComponent._canAttack[_dir] = false;
            _nearMonster = false;
        }
        if (other.tag == "Goal")
        {
            _nearGoal = false;
        }
        if (other.tag == "Rock")
        {
            _playerComponent._canAttack[_dir] = false;
            _nearRock = false;
        }
        if (other.tag == "Lock")
        {
            _playerComponent._canAttack[_dir] = false;
            _nearLock = false;
        }
    }
}
