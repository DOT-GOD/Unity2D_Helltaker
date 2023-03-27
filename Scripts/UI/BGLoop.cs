using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGLoop : MonoBehaviour
{
    public Image _imageBG1;
    public Image _imageBG2;

    public float _backMoveSpeed = 100f;

    void Start()
    {
        LoopBackGroundImage();
    }

    void Update()
    {
        LoopBackGroundImage();
    }
    //��� ���� �Լ�
    void LoopBackGroundImage()
    {
        //��� �̹����� �������� �� ��ġ
        float x = _imageBG1.rectTransform.position.x + _backMoveSpeed * Time.deltaTime;

        //���� �̵� - ���� �κ��� ����
        _imageBG1.rectTransform.position = new Vector3(x, _imageBG1.rectTransform.position.y, 0);
        //���� �κ��� ���纻�� ������ �����Ͽ� �̵�
        _imageBG2.rectTransform.position = new Vector3(x - _imageBG1.rectTransform.sizeDelta.x, _imageBG1.rectTransform.position.y, 0);

        //������ ��� �̵��ϰ� ���̻� �������� ������
        //���纻�� ������ ���� ��ü
        if (x - _imageBG1.rectTransform.sizeDelta.x / 2 > Screen.width)
        {
            Image tempBackImage = _imageBG1;
            _imageBG1 = _imageBG2;
            _imageBG2 = tempBackImage;
            _imageBG1.rectTransform.position = new Vector2(x - _imageBG1.rectTransform.sizeDelta.x, _imageBG1.rectTransform.position.y);
        }
        ;
    }
}
