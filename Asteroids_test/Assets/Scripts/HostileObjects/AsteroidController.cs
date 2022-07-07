using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (SpriteRenderer))]
[RequireComponent(typeof (Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
   [SerializeField, Range(0, 2)] private int size; // 0 - small, 1 - medium, 2 - big

   // private variable 
   private Vector3 _moveVector;

   private void Awake()
   {
      var sprites = Resources.LoadAll<Sprite>($"AsteroidTextures/");
      GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

      switch (size)
      {
         case 0:
            transform.localScale = new Vector3(Constants.SmallAsteroidScale, Constants.SmallAsteroidScale, Constants.SmallAsteroidScale);
            break;
         case 1:
            transform.localScale = new Vector3(Constants.MediumAsteroidScale, Constants.MediumAsteroidScale, Constants.MediumAsteroidScale);
            break;
         case 2:
            transform.localScale = new Vector3(Constants.BigAsteroidScale, Constants.BigAsteroidScale, Constants.BigAsteroidScale);
            break;
         default:
            Debug.LogError("Wrong size");
            break;
      }
   }

   private void Update()
   {
      transform.position += _moveVector * (Time.deltaTime * Constants.AsteroidSpeed);

      if (BordersHandler.IsWithinBorders(transform.position) == false)
         transform.position = BordersHandler.GetPosWithinBorders(transform.position);
   }

   public void SetRandomForce()
   {
      _moveVector.x = Random.Range(0, 2) == 0
         ? Random.Range(-1f, -Constants.AsteroidMinXSpeed)
         : Random.Range(Constants.AsteroidMinXSpeed, 1f); // asteroids shouldn't be too slow
      _moveVector.y = Random.Range(0, 2) == 0
         ? Random.Range(-1f, -Constants.AsteroidMinYSpeed)
         : Random.Range(Constants.AsteroidMinYSpeed, 1f); // asteroids shouldn't be too slow
   }

   public void SetFixedForce(Vector3 moveVector)
   {
      _moveVector = moveVector;
   }

   public void DamageAsteroid()
   {
      if (size > 0)
      {
         // will randomize move vector for child objects
         SpawnHandler.SplitAsteroid(size - 1,
                                    transform.position,
                                    _moveVector * Random.Range(Constants.ChildAsteroidMinSpeedDifference, Constants.ChildAsteroidMaxSpeedDifference));

         PlayerData.Points += (size == 2 ? Constants.PointsPerBigAsteroid : Constants.PointsPerMediumAsteroid);
      }
      else
      {
         PlayerData.Points += (Constants.PointsPerSmallAsteroid);
      }

      SpawnHandler.AsteroidWasDestroyed();
      Return2Pool(size, gameObject);
   }

   // on collision with UFO or player
   public void DestroyAsteroid()
   {
      if (size == 2)
         SpawnHandler.AsteroidWasDestroyed(7); // 1 big + 2 medium + 4 small
      else if (size == 1)
         SpawnHandler.AsteroidWasDestroyed(3); // 1 medium + 2 small
      else
         SpawnHandler.AsteroidWasDestroyed(1);
      
      Return2Pool(size, gameObject);
   }

   private void Return2Pool(int size, GameObject object2Return)
   {
      switch (size)
      {
         case 2: // big
            SoundsManager.PlaySound(Constants.Sounds.BigExplosion);
            ObjectPooler.Return2Pool(Constants.PoolObjects.BigAsteroid, object2Return);
            break;
         case 1: // medium
            SoundsManager.PlaySound(Constants.Sounds.MediumExplosion);
            ObjectPooler.Return2Pool(Constants.PoolObjects.MediumAsteroid, object2Return);
            break;
         case 0: // small
            SoundsManager.PlaySound(Constants.Sounds.SmallExplosion);
            ObjectPooler.Return2Pool(Constants.PoolObjects.SmallAsteroid, object2Return);
            break;
      }
   }
}