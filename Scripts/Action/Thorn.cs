using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public bool _isBurrowed;
    private bool _lastBurrowed = false;
    private BoxCollider2D _collider;

    public bool _playerOnThis = false;

    void Start()
    {
        _collider = this.gameObject.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (_isBurrowed != _lastBurrowed)
            ColliderToggle();
            //Invoke("ColliderToggle",0.2f);

        _lastBurrowed = _isBurrowed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player _playerComponent = other.gameObject.GetComponent<Player>();
        if (other.tag == "Monster" && !_isBurrowed)
        {
            other.GetComponent<Enemy>().Dead();
        }
        if (other.tag == "Player" && !_isBurrowed)
        {
            //if(_playerComponent != null)
            //    _playerComponent.Damaged(0.1f);
            _playerOnThis = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Player _playerComponent = other.gameObject.GetComponent<Player>();
        if (other.tag == "Player" && !_isBurrowed)
        {
            _playerComponent._isOnThorn = false;
            _playerOnThis = false;
        }
    }

    private void ColliderToggle()
    {
        _collider.enabled = !_collider.enabled;

        // 플레이어가 위에 있는데 가시가 숨었을 때
        if (_playerOnThis && !_collider.enabled)
        {
            _playerOnThis = false;
        }
    }

}
