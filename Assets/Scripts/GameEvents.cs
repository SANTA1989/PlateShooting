using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-101)]
public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance = null;

    public Action OnClickThrowPlate;
    public Action OnEnterPlateTrigger;
    public Action OnExitPlateTrigger;
    public Action<float> OnChangeHPPlate;
    public Action DestroyPlateShot;
    public Action DestroyPlateAlive;
    public Action Shot;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
