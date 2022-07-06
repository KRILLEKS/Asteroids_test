using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SpawnHandler : MonoBehaviour
{
   [FormerlySerializedAs("startAsteroidsAmount"),SerializeField] private int startAsteroidsAmountSerializable;

   // private static variables
   private static int _startAsteroidsAmount;
   private static int _currentLevelBigAsteroidsAmount;
   private static int _asteroidsAmountInScene = 0;
   private static SpawnHandler _spawnHandler;

   private void Awake()
   {
      _startAsteroidsAmount = startAsteroidsAmountSerializable;
      _spawnHandler = this;
      
      InitializeHandler();
   }

   private static void InitializeHandler()
   {
      for (int i = 0; i < _startAsteroidsAmount; i++)
         SpawnBigAsteroid();

      _currentLevelBigAsteroidsAmount = _startAsteroidsAmount + 1;

      SpawnUFO();
   }
   
   private static void SpawnBigAsteroid()
   {
      var obj = ObjectPooler.SpawnFromPool(Constants.PoolObjects.BigAsteroid, BordersHandler.GetEdgePosition(), Quaternion.identity);
      obj.GetComponent<AsteroidController>().SetRandomForce();
      // 1 big
      // 2 medium
      // 4 small
      // 7 in total
      _asteroidsAmountInScene += 7;
   }

   private static void SpawnNonBigAsteroid(Constants.PoolObjects asteroidType, Vector3 position, Vector3 moveVector)
   {
      var obj = ObjectPooler.SpawnFromPool(asteroidType, position, Quaternion.identity);
      obj.GetComponent<AsteroidController>().SetFixedForce(moveVector);
   }

   public static void SplitAsteroid(int childAsteroidsSize, Vector3 position, Vector3 moveVector)
   {
      SpawnNonBigAsteroid(childAsteroidsSize == 0 ? Constants.PoolObjects.SmallAsteroid : Constants.PoolObjects.MediumAsteroid,
                          position,
                          Quaternion.AngleAxis(-45, Vector3.forward) * moveVector);
      SpawnNonBigAsteroid(childAsteroidsSize == 0 ? Constants.PoolObjects.SmallAsteroid : Constants.PoolObjects.MediumAsteroid,
                          position,
                          Quaternion.AngleAxis(45, Vector3.forward) * moveVector);
   }

   // basically when we destroy asteroid we decrease amount by 1
   // but if it collides with UFO or player we can decrease it on bigger value
   public static void AsteroidWasDestroyed(int amount = 1)
   {
      _asteroidsAmountInScene--;

      if (_asteroidsAmountInScene == 0)
         _spawnHandler.StartCoroutine(AsteroidSpawnCoroutine());
   }

   // invokes after destroying the old one
   public static void SpawnUFO()
   {
      _spawnHandler.StartCoroutine(SpawnUFOCoroutine());
   }

   private static IEnumerator SpawnUFOCoroutine()
   {
      yield return new WaitForSeconds(Random.Range(Constants.MinUFOSpawnTime, Constants.MaxUFOSpawnTime));

      ObjectPooler.SpawnFromPool(Constants.PoolObjects.UFO, BordersHandler.GetRandomUFOPosition(), Quaternion.identity);
   }

   private static IEnumerator AsteroidSpawnCoroutine()
   {
      yield return new WaitForSeconds(Constants.Time2SpawnNewLevel);

      for (int i = 0; i < _currentLevelBigAsteroidsAmount; i++)
         SpawnBigAsteroid();

      _currentLevelBigAsteroidsAmount++;
   }

   public static void ResetValues()
   {
      _spawnHandler.StopAllCoroutines();
      InitializeHandler();
   }
}