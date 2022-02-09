using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plate : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curveDamage;
    [SerializeField] private float _timeForceShot;
    [SerializeField] private GameObject _prefFX;
    
    private float _hitPoints = 1f;
    [SerializeField] private float _timeFly = 0;
    private float _countPhysicsframePerSecond;
    private bool _didForceShot = false;

    private void Start()
    {
        _countPhysicsframePerSecond = 1000f * Time.fixedDeltaTime;
    }

    private void Update()
    {
        _timeFly += Time.deltaTime;

        if (_hitPoints <= 0)
        {
           DestroyShot();
        } else if ( _timeForceShot < _timeFly && !_didForceShot )
        {
            if (Random.Range(0, 1f) > _hitPoints)
            {
                DestroyShot();
            }
            _didForceShot = true;
        }
    }

    private void DestroyShot()
    {
        GameEvents.Instance.Shot?.Invoke();
        Instantiate(_prefFX, transform.position, Quaternion.identity);
        GameEvents.Instance.DestroyPlateShot.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        _hitPoints -= _curveDamage.Evaluate(_timeFly) / _countPhysicsframePerSecond;
        GameEvents.Instance.OnChangeHPPlate?.Invoke(_hitPoints);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameEvents.Instance.DestroyPlateAlive?.Invoke();
        Destroy(gameObject);
    }
}
