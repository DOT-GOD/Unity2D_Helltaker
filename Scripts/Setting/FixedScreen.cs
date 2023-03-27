using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScreen : MonoBehaviour
{
    private void Start()
    {
        SetResolution();
    }

    // �ػ� ���� �Լ�
    public void SetResolution()
    {
        int setWidth = 1920; // ȭ�� �ʺ�
        int setHeight = 1080; // ȭ�� ����

        //�ػ󵵸� �������� ���� ����
        //3��° �Ķ���ʹ� Ǯ��ũ�� ��带 ���� > true : Ǯ��ũ��, false : â���
        Screen.SetResolution(setWidth, setHeight, false);
    }
}
