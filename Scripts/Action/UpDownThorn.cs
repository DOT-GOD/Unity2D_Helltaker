using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownThorn : MonoBehaviour
{
    private Thorn _thornComponent;
    private Player _playerComponent;
    private int _lastHp;
    private bool _lastBorrwed;
    public AnimatorController _aniCon;

    void Start()
    {
        // 부모 오브젝트의 Thorn Component
        _thornComponent = this.gameObject.GetComponentInParent<Thorn>();

        // 현재 플레이어의 Player Component
        _playerComponent = GameObject.FindWithTag("Player").GetComponent<Player>();

        _lastBorrwed = !_thornComponent._isBurrowed;
        _lastHp = _playerComponent._playerHp;

        _aniCon = this.gameObject.AddComponent<AnimatorController>();

    }

    void Update()
    {
        //if (_lastHp > _playerComponent._playerHp)
        //{
        //    _thornComponent._isBurrowed = !_thornComponent._isBurrowed;
        //    _lastHp = _playerComponent._playerHp;
        //}
            

        if(_lastBorrwed != _thornComponent._isBurrowed)
        {
            if(_lastBorrwed)
                _aniCon.Play(1);
            else
                _aniCon.Play(2);
        }
        _lastBorrwed = _thornComponent._isBurrowed;
    }
}
