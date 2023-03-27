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
    //배경 루프 함수
    void LoopBackGroundImage()
    {
        //배경 이미지가 움직여야 될 위치
        float x = _imageBG1.rectTransform.position.x + _backMoveSpeed * Time.deltaTime;

        //원본 이동 - 남는 부분이 있음
        _imageBG1.rectTransform.position = new Vector3(x, _imageBG1.rectTransform.position.y, 0);
        //남는 부분은 복사본이 영역을 차지하여 이동
        _imageBG2.rectTransform.position = new Vector3(x - _imageBG1.rectTransform.sizeDelta.x, _imageBG1.rectTransform.position.y, 0);

        //원본이 모두 이동하고 더이상 비쳐지지 않을때
        //복사본과 원본을 서로 교체
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
