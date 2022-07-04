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

   [FormerlySerializedAs("_pools"), SerializeField] private List<Pool> pools;
   [SerializeField] private Transform itemsHolderSerializable; // will be parent for items in pool to keep hierarchy clean

   // private static variables
   private static Transform _itemsHolder;
   private static Dictionary<Constants.PoolObjects, Queue<GameObject>> _poolDictionary = new ();

   private void Awake()
   {
      _itemsHolder = itemsHolderSerializable;
      
      foreach (var pool in pools)
      {
         var objectPool = new Queue<GameObject>();

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
      }

      return object2Spawn;
   }

   public static void Return2Pool(Constants.PoolObjects name, GameObject gameObject)
   {
      _poolDictionary[name].Enqueue(gameObject);
      gameObject.SetActive(false);
   }
}