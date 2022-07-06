using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
   // private variables
   private SpriteRenderer _playerSprite;
   private bool _isInvulnerable = false;
   private float _invulnerabilityStartTime = float.MinValue;

   private void Awake()
   {
      _playerSprite = GetComponent<SpriteRenderer>();
   }

   private IEnumerator OnBecameInvulnerable()
   {
      _invulnerabilityStartTime = Time.time;
      _isInvulnerable = true;
      
      // TODO: make smooth blink (lerp). I can do it with Lerp (cause DOTween is forbidden)
      while (Time.time < _invulnerabilityStartTime + Constants.InvulnerabilityTime)
      {
         _playerSprite.color = new Color(_playerSprite.color.r,_playerSprite.color.g, _playerSprite.color.b,Constants.BlinkingAlpha);

         yield return new WaitForSeconds(Constants.InvulnerabilityTime / Constants.BlinkingTimes / 2 + 0.01f); // we add +0.01 cause we can iterate one more time so we prevent that
         
         _playerSprite.color = new Color(_playerSprite.color.r,_playerSprite.color.g, _playerSprite.color.b, 1);
         
         yield return new WaitForSeconds(Constants.InvulnerabilityTime / Constants.BlinkingTimes / 2);
      }

      _isInvulnerable = false;
   }
      
   private void OnTriggerEnter2D(Collider2D colliderInfo)
   {
      if (_isInvulnerable || colliderInfo.gameObject.layer != LayerMask.NameToLayer($"HostileObject"))
         return;

      PlayerData.LifeAmount--;

      if (PlayerData.LifeAmount > 0)
      {
         StartCoroutine(OnBecameInvulnerable());
         transform.position = BordersHandler.GetRandomPosition();
         PlayerMovementController.StopMovement();
      }
      else
      {
         // TODO: 
         Debug.Log("DEATH");
      }
   }
}
