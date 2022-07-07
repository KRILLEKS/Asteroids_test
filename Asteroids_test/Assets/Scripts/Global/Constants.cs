using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// there be all values that can affect gameplay globally and which can be changed
// this values are here to prevent searching for them in scene
// the reason why enum is here (there will be all enums but in this project I have only 2) cause u can add extra values and u don't need to search for it
// because you know that all enums are in one object
public class Constants
{
   // asteroid scales
   public const float BigAsteroidScale = 0.2f;
   public const float MediumAsteroidScale = 0.15f;
   public const float SmallAsteroidScale = 0.1f;

   // objects speed
   public const float AsteroidSpeed = 1.2f;
   public const float PlayerBulletSpeed = 3.5f;
   
   // asteroid randomize info
   public const float AsteroidMinXSpeed = 0.4f; // from 1 to 0
   public const float AsteroidYSpeed = 1f;
   // difference between child and parent
   public const float ChildAsteroidMinSpeedDifference = 0.8f;
   public const float ChildAsteroidMaxSpeedDifference = 1.2f;
   public const float AsteroidSplitAngle = 45f;
   
   // points per object
   public const float PointsPerBigAsteroid = 20;
   public const float PointsPerMediumAsteroid = 50;
   public const float PointsPerSmallAsteroid = 100;
   public const float PointsPerUFO = 200;
   
   // player data
   public const int LifeAmount = 5;
   public const float InvulnerabilityTime = 3f; // in seconds
   public const int BlinkingTimes = 5; // how many times blinking per invulnerability period
   public const float BlinkingAlpha = 0.5f; // range between 0 and 1

   public const float Time2SpawnNewLevel = 2; // in seconds
   
   // UFO data
   public const float MinUFOSpawnTime = 20f; // in seconds
   public const float MaxUFOSpawnTime = 40f; // in seconds
   public const float OffsetFromBorder = 20f; // in percent 
   public const float UFOSpeed = 2f;
   public const float UFOBulletSpeed = 2f;
   public const float UFOMinTime2Shoot = 2; // in seconds
   public const float UFOMaxTime2Shoot = 5; // in seconds
   
   // pause menu
   public const float PauseMenuOpenSpeed = 1.2f; // in seconds
   
   public enum PoolObjects
   {
      BigAsteroid,
      MediumAsteroid,
      SmallAsteroid,
      PlayerBullet,
      UFOBullet,
      UFO
   }
   public enum Sounds
   {
      PlayerFire,
      UFO,
      Thruster,
      SmallExplosion,
      MediumExplosion,
      BigExplosion,
   }
}
