using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public bool _isSelected;
    public GameObject _selcetedMenu;                  // 지정필요 : 밝은 색 메뉴 오브젝트
    public GameObject _unselcetedMenu;                // 지정필요 : 어두운 색 메뉴 오브젝트

    void Start()
    {
    }

    void Update()
    {
        if(_isSelected)
        {
            _selcetedMenu.SetActive(true);
            _unselcetedMenu.SetActive(false);
        }
        else
        {
            _selcetedMenu.SetActive(false);
            _unselcetedMenu.SetActive(true);
        }
    }
}
