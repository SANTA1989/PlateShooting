using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class InputListener : MonoBehaviour
{
   [SerializeField] private Transform _pointTarget;
   [SerializeField] private float _sence = .05f;
   [SerializeField] private float _maxDotBound;

   private float _distanceCamera;
   private Camera _camera;
   private Coroutine _corHandler;
   private Vector3 _lastFingerPosition;
   private Vector3 _centerScreenProection;
   private float _lastDot = 1f;
   

   private void Start()
   {
      _camera = Camera.main;
      _distanceCamera = _pointTarget.position.z - _camera.transform.position.z;
      _centerScreenProection = new Vector3(Screen.width / 2, Screen.height / 2, _distanceCamera);
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         _corHandler = StartCoroutine(HandlerMouse());
      } else if ( Input.GetMouseButtonUp(0) )
      {
         if (_corHandler != null)
         {
            StopCoroutine(_corHandler);
            _corHandler = null;
         }
      }
   }

   private IEnumerator HandlerMouse()
   {
      var wait = new WaitForEndOfFrame();
      _lastFingerPosition = Input.mousePosition;
      _lastDot = Quaternion.Dot(Quaternion.Euler(Vector3.forward), 
         Quaternion.LookRotation(_camera.transform.forward));
      
      while (Input.GetMouseButton(0))
      {
         var move = (Input.mousePosition - _lastFingerPosition) * _sence;
         
         move = CheckBounds(move);
         
         _camera.transform.rotation = Quaternion.LookRotation(_camera.transform.forward + move);
         _pointTarget.position = _camera.ScreenToWorldPoint(_centerScreenProection);
         _lastFingerPosition = Input.mousePosition;
         yield return wait;
      }

      _corHandler = null;
   }

   private Vector3 CheckBounds(Vector3 move)
   {
      float dot = Quaternion.Dot(Quaternion.Euler(Vector3.forward), Quaternion.LookRotation(_camera.transform.forward + move));
      if ( dot < _maxDotBound && dot <= _lastDot )
      {
         return  Vector3.zero;
      }

      _lastDot = dot;
      return move;
   }
   
   
}
