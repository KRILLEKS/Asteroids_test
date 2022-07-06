using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBulletController : MonoBehaviour
{
   // private variables
   private Vector3 _previousPos;
   private float _distanceTraveled;
   private bool _needs2Reset = true;

   private void Update()
   {
      // set start pos
      if (_needs2Reset)
      {
         _previousPos = transform.position;
         _needs2Reset = false;
         _distanceTraveled = 0;
         return;
      }
      
      // move
      transform.position += transform.right * (Constants.PlayerBulletSpeed * Time.deltaTime);
      _distanceTraveled += Vector3.Distance(transform.position, _previousPos);
      _previousPos = transform.position;

      // return to pool when reached max distance
      if (_distanceTraveled > BordersHandler.TopRightCorner.x * 2)
      {
         _needs2Reset = true;
         ObjectPooler.Return2Pool(Constants.PoolObjects.PlayerBullet, gameObject);
      }
      
      // check borders
      if (BordersHandler.IsWithinBorders(transform.position) == false)
      {
         transform.position = BordersHandler.GetPosWithinBorders(transform.position);
         _previousPos = transform.position;
      }
   }

   private void OnTriggerEnter2D(Collider2D colliderInfo)
   {
      if (colliderInfo.gameObject.layer != LayerMask.NameToLayer($"HostileObject"))
         return;

      _needs2Reset = true;
      
      if (colliderInfo.CompareTag("Asteroid"))
      {
         colliderInfo.GetComponent<AsteroidController>().DamageAsteroid();
         ObjectPooler.Return2Pool(Constants.PoolObjects.PlayerBullet, gameObject);
      }
   }
}
