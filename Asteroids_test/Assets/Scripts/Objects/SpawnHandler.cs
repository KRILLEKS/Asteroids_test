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
   
   private void Awake()
   {
      for (int i = 0; i < startAsteroidsAmount; i++)
         SpawnAsteroid();
   }

   private void SpawnAsteroid()
   {
      var obj = ObjectPooler.SpawnFromPool(Constants.PoolObjects.BigAsteroid, BordersHandler.GetEdgePosition(), Quaternion.identity);
      obj.GetComponent<AsteroidController>().GenerateMoveVector();
   }
}
