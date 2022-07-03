using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
   [System.Serializable]
   public class Pool
   {
      public string name;
      public GameObject prefab;
      public int startSize;
   }
   
   // private variables
   private List<Pool> _pools;
}
