using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerPlate : MonoBehaviour
{
   [SerializeField] private Rigidbody _prefPlate;
   [SerializeField] private Transform[] _pointSpawns;
   [SerializeField] private float _mixOffsetAngleX;
   [SerializeField] private float _maxOffsetAngleX;
   [SerializeField] private float _timeoutThrowPlate;
   
   private float _forceThrow;
   private Coroutine _corTimeout;

   private void Start()
   {
      GameEvents.Instance.OnClickThrowPlate += CreatePlate;
      ConfigureBoundsSpawner();
      ConfigureGravityFly();
   }

   public void CreatePlate()
   {
      if (_corTimeout == null)
      {
         var selectPoint = _pointSpawns[Random.Range(0, _pointSpawns.Length)];
         var randomiseAngle = Random.Range(_mixOffsetAngleX, _maxOffsetAngleX);
         selectPoint.eulerAngles = new Vector3( randomiseAngle, selectPoint.localEulerAngles.y, selectPoint.localEulerAngles.z );
         
         var plate = Instantiate(_prefPlate, selectPoint.position, selectPoint.rotation);
         plate.AddForce(selectPoint.forward * _forceThrow);
         
         StartCoroutine(Timeout());
      }
   }

   private void ConfigureBoundsSpawner()
   {
      var camera = Camera.main;

      var pointLeft = camera.ScreenToWorldPoint(
         new Vector3(0, Screen.height / 2, transform.position.z - camera.transform.position.z));
      var pointRight = camera.ScreenToWorldPoint(
         new Vector3(Screen.width, Screen.height / 2, transform.position.z - camera.transform.position.z));

      if (_pointSpawns[0].position.x < 0)
      {
         _pointSpawns[0].position = new Vector3( pointLeft.x, _pointSpawns[0].position.y, _pointSpawns[0].position.z );
         _pointSpawns[1].position = new Vector3( pointRight.x, _pointSpawns[1].position.y, _pointSpawns[1].position.z );
      } else
      {
         _pointSpawns[0].position = new Vector3( pointRight.x, _pointSpawns[0].position.y, _pointSpawns[0].position.z );
         _pointSpawns[1].position = new Vector3( pointLeft.x, _pointSpawns[1].position.y, _pointSpawns[1].position.z );
      }
   }

   private void ConfigureGravityFly()
   {
      float averageAngle = (Mathf.Abs(_mixOffsetAngleX) + Mathf.Abs(_maxOffsetAngleX)) * .5f; 
      var distance = Vector3.Distance(_pointSpawns[0].position, _pointSpawns[1].position);
      _forceThrow = 50f * distance / 5f * 2f; 
      var g = Mathf.Pow(_forceThrow / 50f, 2f) *  Mathf.Sin(Mathf.Deg2Rad * averageAngle * 2f) / distance;
      Physics.gravity = new Vector3(Physics.gravity.x, -g, Physics.gravity.z);
   }
   
   private IEnumerator Timeout()
   {
      yield return new WaitForSeconds(_timeoutThrowPlate);
      _corTimeout = null;
   }

   private void OnDestroy()
   {
      if (GameEvents.Instance.OnClickThrowPlate != null)
      {
         GameEvents.Instance.OnClickThrowPlate -= CreatePlate;
      }
      
   }
}
