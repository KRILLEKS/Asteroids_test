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
         if (_points <= value)
            return;
         
         _points = value;
         UIController.UpdatePointsInfo();
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
         UIController.UpdateLifeInfo();
      }
      get
      {
         return _lifeAmount;
      }
   }

   // private static variable
   private static float _points = 0;
   private static int _lifeAmount = Constants.LifeAmount;
}