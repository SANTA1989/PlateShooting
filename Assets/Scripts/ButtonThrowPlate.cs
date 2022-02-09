using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonThrowPlate : MonoBehaviour
{
   [SerializeField] private Button _button;
   
   private void Start()
   {
      _button.onClick.AddListener(OnClick);

      GameEvents.Instance.OnClickThrowPlate += Hide;
      GameEvents.Instance.DestroyPlateAlive += Show;
      GameEvents.Instance.DestroyPlateShot += Show;
   }

   private void OnClick()
   {
      GameEvents.Instance.OnClickThrowPlate?.Invoke();
   }

   private void Hide()
   {
      _button.gameObject.SetActive(false);
   }

   private void Show()
   {
      _button.gameObject.SetActive(true);
   }

   private void OnDestroy()
   {
      GameEvents.Instance.OnClickThrowPlate -= Hide;
      GameEvents.Instance.DestroyPlateAlive -= Show;
      GameEvents.Instance.DestroyPlateShot -= Show;
   }
}
