using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBulletController : Bullet
{
   // private variables
   private GameObject _player;
   private Vector3 _flyVector;

   private void Awake()
   {
      _player = GameObject.FindWithTag($"Player");
   }

   private void Update()
   {
      // move
      transform.position += _flyVector * (Constants.UFOBulletSpeed * Time.deltaTime);

      CheckState();
   }

   internal override void CheckForReset()
   {
      // set start pos
      if (_needs2Reset)
      {
         _previousPos = transform.position;
         _needs2Reset = false;
         _distanceTraveled = 0;
         _flyVector = Vector3.Normalize(_player.transform.position - transform.position);
      }
   }

   private void OnTriggerEnter2D(Collider2D colliderInfo)
   {
      if (colliderInfo.CompareTag("Player"))
      {
         _needs2Reset = true;
         ObjectPooler.Return2Pool(Constants.PoolObjects.UFOBullet, gameObject);
      }
   }
}