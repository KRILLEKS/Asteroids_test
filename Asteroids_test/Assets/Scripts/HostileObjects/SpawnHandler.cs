using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SpawnHandler : MonoBehaviour
{
   [SerializeField] private int startAsteroidsAmount;
   
   // private static variables
   private static int _currentLevelBigAsteroidsAmount;
   private static int _asteroidsAmountInScene = 0;
   private static SpawnHandler _spawnHandler;
   
   private void Awake()
   {
      _spawnHandler = this;
      
      for (int i = 0; i < startAsteroidsAmount; i++)
         SpawnBigAsteroid();
      
      _currentLevelBigAsteroidsAmount = startAsteroidsAmount + 1;
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
      SpawnNonBigAsteroid(childAsteroidsSize == 0 ? Constants.PoolObjects.SmallAsteroid : Constants.PoolObjects.MediumAsteroid, position, Quaternion.AngleAxis(-45, Vector3.forward) * moveVector);
      SpawnNonBigAsteroid(childAsteroidsSize == 0 ? Constants.PoolObjects.SmallAsteroid : Constants.PoolObjects.MediumAsteroid, position, Quaternion.AngleAxis(45, Vector3.forward) * moveVector);
   }

   public static void AsteroidWasDestroyed()
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

      ObjectPooler.SpawnFromPool(Constants.PoolObjects.UFO, BordersHandler.GetEdgePosition(), Quaternion.identity);
   }
   
   private static IEnumerator AsteroidSpawnCoroutine()
   {
      yield return new WaitForSeconds(Constants.Time2SpawnNewLevel);

      for (int i = 0; i < _currentLevelBigAsteroidsAmount; i++)
         SpawnBigAsteroid();

      _currentLevelBigAsteroidsAmount++;
   }
}
