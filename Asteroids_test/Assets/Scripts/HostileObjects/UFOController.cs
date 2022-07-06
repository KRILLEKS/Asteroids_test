using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (Rigidbody2D))]
public class UFOController : MonoBehaviour
{
   // private static variables
   private static float _xSpeed;
   private static bool _need2ChangeInfo = true;

   private void Awake()
   {
      StartCoroutine(ShootCoroutine());
   }

   private void Update()
   {
      if (_need2ChangeInfo)
      {
         _xSpeed = (Random.Range(0, 2) == 0 ? -1 : 1) * Constants.UFOSpeed;
         _need2ChangeInfo = false;
         StartCoroutine(ShootCoroutine());
      }
      
      // move
      transform.position += new Vector3(_xSpeed, 0, 0) * Time.deltaTime;

      // check borders
      if (BordersHandler.IsWithinBorders(transform.position) == false)
         transform.position = BordersHandler.GetPosWithinBorders(transform.position);
      
      SoundsManager.PlaySound(Constants.Sounds.UFO);
   }

   private IEnumerator ShootCoroutine()
   {
      while (true)
      {
         yield return new WaitForSeconds(Random.Range(Constants.UFOMinTime2Shoot, Constants.UFOMaxTime2Shoot));
         ObjectPooler.SpawnFromPool(Constants.PoolObjects.UFOBullet, transform.position, Quaternion.identity);
      }
   }

   public void DestroyUFO(bool receivePoints)
   {
      if (receivePoints)
         PlayerData.Points += Constants.PointsPerUFO;
      
      SpawnHandler.SpawnUFO();
      _need2ChangeInfo = true;
      ObjectPooler.Return2Pool(Constants.PoolObjects.UFO, gameObject);
   }
   
   private void OnTriggerEnter2D(Collider2D collisionInfo)
   {
      if (collisionInfo.gameObject.CompareTag($"Asteroid"))
      {
         collisionInfo.gameObject.GetComponent<AsteroidController>().DestroyAsteroid();
         DestroyUFO(false);
      }
   }
}