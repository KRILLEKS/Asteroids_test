using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI pointsTextSerializable;
   [SerializeField] private Transform lifeContainerSerializable;
   
   // private static variables
   private static TextMeshProUGUI _pointsText;
   private static Transform _lifeContainer;

   private void Awake()
   {
      _pointsText = pointsTextSerializable;
      _lifeContainer = lifeContainerSerializable;

      if (_lifeContainer.childCount < 1)
         Debug.LogError("Wrong UI life amount");

      for (int i = 1; i < _lifeContainer.childCount; i++)
         Destroy(_lifeContainer.GetChild(i).gameObject);

      for (int i = 1; i < Constants.LifeAmount; i++)
         Instantiate(_lifeContainer.GetChild(0), _lifeContainer);
   }

   public static void UpdatePointsInfo()
   {
      _pointsText.text = PlayerData.Points.ToString();
   }

   public static void UpdateLifeInfo()
   {
      _lifeContainer.GetChild(PlayerData.LifeAmount).gameObject.SetActive(false);
   }

   public static void ResetValues()
   {
      for (int i = 0; i < _lifeContainer.childCount; i++)
         _lifeContainer.transform.GetChild(i).gameObject.SetActive(true);

      _pointsText.text = 0.ToString();
   }
}
