using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public bool _isSelected;
    public GameObject _selcetedMenu;                  // �����ʿ� : ���� �� �޴� ������Ʈ
    public GameObject _unselcetedMenu;                // �����ʿ� : ��ο� �� �޴� ������Ʈ

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
