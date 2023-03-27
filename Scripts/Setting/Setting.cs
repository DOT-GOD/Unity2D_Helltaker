using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject _controlButton;
    public GameObject _settingWindow;
    public GameObject _settingButton;

    void Start()
    {
    }

    void Update()
    {
    }

    public void ActiveSettingWindow()
    {
        _settingWindow.SetActive(true);
        _settingButton.SetActive(false);
        _controlButton.SetActive(false);
    }


    public void QuitButton()
    {
        _settingWindow.SetActive(false);
        _settingButton.SetActive(true);
        _controlButton.SetActive(true);
    }

    public void LoadButton()
    {
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ResetButton()
    {
        Application.Quit();
    }
}
