using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 20.0f)]
    public float _shakeTime = 0.5f;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    public float _shakeSpeed = 0.3f;

    private float _currentTime = 0.0f;
    private float _lastShakeTime = 0.0f;

    private Vector3 _initPosition;

    void Start()
    {
        _initPosition = this.transform.position;
    }

    void Update()
    {
        //�ð�������
        _currentTime += Time.deltaTime;
        if(_currentTime > _lastShakeTime + _shakeTime *2)
            _lastShakeTime = _currentTime;

        if (_shakeTime > _currentTime - _lastShakeTime)
        {
            //��ǥ�������� �̵�
            this.transform.position
                = Vector2.MoveTowards(this.transform.position,
                this.transform.position + new Vector3(0,1,0),
                Time.deltaTime * _shakeSpeed);
        }
        else
        {
            //��ǥ�������� �̵�
            this.transform.position
                = Vector2.MoveTowards(this.transform.position,
                this.transform.position + new Vector3(0, -1, 0),
                Time.deltaTime * _shakeSpeed);
        }

        // ó�� �������� �ʹ� �ָ����� ����
        if (this.transform.position.y + 0.2f < _initPosition.y
            || this.transform.position.y - 0.2f > _initPosition.y)
            this.transform.position = _initPosition;
    }
}
