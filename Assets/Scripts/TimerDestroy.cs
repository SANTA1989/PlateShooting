using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField] private float _timeLive;

    private IEnumerator Start()
    {
        yield return  new WaitForSeconds(_timeLive);
        Destroy(gameObject);
    }
}
