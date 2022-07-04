using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// there be all values that can affect gameplay globally and which can be changed
// this values are here to prevent searching for them in scene
// the reason why enum is here (there will be all enums but in this project I have only 1) cause u can add extra values and u don't need to search for it
public class Constants
{
   public const float BigAsteroidScale = 0.2f;
   public const float MediumAsteroidScale = 0.15f;
   public const float SmallAsteroidScale = 0.1f;

   public const float AsteroidSpeed = 1.2f;
   
   public enum PoolObjects
   {
      BigAsteroid,
      MediumAsteroid,
      SmallAsteroid,
      Bullet
   }
}
