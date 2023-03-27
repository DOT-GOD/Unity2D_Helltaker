using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator _animator = null;

    void Start()
    {
        //애니메이터 초기화
        _animator = this.GetComponentInChildren<Animator>();
        if(_animator == null)
            _animator = this.GetComponent<Animator>();

    }

    void Update()
    {
        
    }

    public void Play(int num)
    {
        if(_animator != null)
        {
            _animator.SetInteger("State", num);

            Invoke("Stop", 0.5f);
        }
    }
    private void Stop()
    {
        if (_animator != null)
            _animator.SetInteger("State", 0);
    }
}
