using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
        GameEvents.Instance.OnEnterPlateTrigger += ShowBar;
        GameEvents.Instance.OnExitPlateTrigger += HideBar;
        GameEvents.Instance.OnChangeHPPlate += OnChangeHP;
        GameEvents.Instance.OnClickThrowPlate += ResetProgress;
        GameEvents.Instance.DestroyPlateShot += ResetProgress;
        GameEvents.Instance.DestroyPlateShot += HideBar;
        GameEvents.Instance.DestroyPlateAlive += ResetProgress;
        GameEvents.Instance.DestroyPlateAlive += HideBar;
    }

    private void ResetProgress()
    {
        _image.fillAmount = 0f;
    }
    
    private void ShowBar()
    {
        _image.enabled = true;
    }
    
    private void HideBar()
    {
        _image.enabled = false;
    }

    private void OnChangeHP(float normalizeHP)
    {
        _image.fillAmount = 1f - normalizeHP;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnEnterPlateTrigger -= ShowBar;
        GameEvents.Instance.OnExitPlateTrigger -= HideBar;
        GameEvents.Instance.OnChangeHPPlate -= OnChangeHP;
        GameEvents.Instance.OnClickThrowPlate -= ResetProgress;
        GameEvents.Instance.DestroyPlateShot -= ResetProgress;
        GameEvents.Instance.DestroyPlateShot -= HideBar;
        GameEvents.Instance.DestroyPlateAlive -= ResetProgress;
        GameEvents.Instance.DestroyPlateAlive -= HideBar;
    }
}
