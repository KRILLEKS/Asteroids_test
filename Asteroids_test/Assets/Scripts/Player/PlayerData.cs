using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
   // public static variables
   public static float Points
   {
      set
      {
         if (_points >= value)
            return;
         
         _points = value;
         InGameUIController.UpdatePointsInfo();
      }
      get
      {
         return _points;
      }
   }

   public static int LifeAmount
   {
      set
      {
         if (_lifeAmount <= value)
            return;
         
         _lifeAmount = value;
         InGameUIController.UpdateLifeInfo();
      }
      get
      {
         return _lifeAmount;
      }
   }

   public static bool IsOnlyKeyboard;
      
   // private static variable
   private static float _points = 0;
   private static int _lifeAmount = Constants.LifeAmount;

   public static void ResetValues()
   {
      _points = 0;
      _lifeAmount = Constants.LifeAmount;
   }
}