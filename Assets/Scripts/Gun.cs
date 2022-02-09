using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   [SerializeField] private ParticleSystem _particle;
   [SerializeField] private Transform _pointInstantiate;

   private void Start()
   {
      GameEvents.Instance.Shot += Shot;
   }

   private void Shot()
   {
      Instantiate(_particle, _pointInstantiate.position, _pointInstantiate.rotation, _pointInstantiate);
   }

   private void OnTriggerEnter(Collider other)
   {
      GameEvents.Instance.OnEnterPlateTrigger?.Invoke();
   }

   private void OnTriggerExit(Collider other)
   {
      GameEvents.Instance.OnExitPlateTrigger?.Invoke();
   }

   private void OnDestroy()
   {
      GameEvents.Instance.Shot -= Shot;
   }
}
