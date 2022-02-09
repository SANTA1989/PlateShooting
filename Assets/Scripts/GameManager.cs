using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
   public static GameManager Instance = null;

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
