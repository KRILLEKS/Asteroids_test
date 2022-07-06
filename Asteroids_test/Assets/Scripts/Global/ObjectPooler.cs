using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{
   [System.Serializable]
   public class Pool
   {
      public Constants.PoolObjects name;
      public GameObject prefab;
      public int startSize;
   }

   [SerializeField] private List<Pool> poolsSerializable;
   [SerializeField] private Transform itemsHolderSerializable; // will be parent for items in pool to keep hierarchy clean

   // private static variables
   private static List<Pool> _pools;
   private static Transform _itemsHolder;
   private static Dictionary<Constants.PoolObjects, Queue<GameObject>> _poolDictionary;

   private void Awake()
   {
      _pools = poolsSerializable;
      _itemsHolder = itemsHolderSerializable;
      
      InitializePool();
   }

   private static void InitializePool()
   {
      _poolDictionary = new Dictionary<Constants.PoolObjects, Queue<GameObject>>();
      
      foreach (var pool in _pools)
      {
         if (pool.prefab == null)
            continue;
         
         var objectPool = new Queue<GameObject>();

         if (pool.startSize < 1)
            pool.startSize = 1;

         for (int i = 0; i < pool.startSize; i++)
         {
            var obj = Instantiate(pool.prefab, _itemsHolder);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
         }

         _poolDictionary.Add(pool.name, objectPool);
      }
   }

   public static GameObject SpawnFromPool(Constants.PoolObjects name, Vector3 position, Quaternion rotation)
   {
      GameObject object2Spawn;
      // > 1 because when I spawn new object I just duplicate the top one of the queue
      // alternatively I could load them from Resources folder
      if (_poolDictionary[name].Count > 1)
      {
         object2Spawn = _poolDictionary[name].Dequeue();

         object2Spawn.transform.position = position;
         object2Spawn.transform.rotation = rotation;
         
         object2Spawn.SetActive(true);
      }
      else
      {
         object2Spawn = Instantiate(_poolDictionary[name].Peek(), position, rotation, _itemsHolder);
         object2Spawn.SetActive(true);
      }

      return object2Spawn;
   }

   public static void Return2Pool(Constants.PoolObjects name, GameObject gameObject)
   {
      _poolDictionary[name].Enqueue(gameObject);
      gameObject.SetActive(false);
   }

   public static void ResetValues()
   {
      // we could return everything to their pools depending on their names (it would be scalable cause we'll base on prefab name)
      // but I chose the simple way because it's doesn't matter here
      for (int i = 0; i < _itemsHolder.transform.childCount; i++)
         Destroy(_itemsHolder.transform.GetChild(i).gameObject);
      
      InitializePool();
   }
}