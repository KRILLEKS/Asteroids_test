using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
   [SerializeField] private float offset;
   [SerializeField] private float fireSpeed = 3; // bullets per second

   // private variables
   private float _previousShotTime = float.MinValue;

   // invokes on left click
   public void Shoot()
   {
      if (Time.time - _previousShotTime > 1 / fireSpeed == false)
         return;

      ObjectPooler.SpawnFromPool(Constants.PoolObjects.PlayerBullet, transform.position + transform.right * offset, transform.rotation);
      _previousShotTime = Time.time;
      
      SoundsManager.PlaySound(Constants.Sounds.PlayerFire);
   }

   private void OnDrawGizmos()
   {
      if (Application.isPlaying)
         return;

      Gizmos.DrawSphere(transform.position + transform.right * offset, 0.05f);
   }
}