using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBulletController : Bullet
{
   private void Update()
   {
      // move
      transform.position += transform.right * (Constants.PlayerBulletSpeed * Time.deltaTime);
      
      CheckState();
   }

   private void OnTriggerEnter2D(Collider2D colliderInfo)
   {
      if (colliderInfo.gameObject.layer != LayerMask.NameToLayer($"HostileObject"))
         return;

      _needs2Reset = true;
      
      if (colliderInfo.CompareTag($"Asteroid"))
      {
         colliderInfo.GetComponent<AsteroidController>().DamageAsteroid();
         ObjectPooler.Return2Pool(Constants.PoolObjects.PlayerBullet, gameObject);
      }
      else if (colliderInfo.CompareTag($"UFO"))
      {
         colliderInfo.GetComponent<UFOController>().DestroyUFO(true);
         ObjectPooler.Return2Pool(Constants.PoolObjects.PlayerBullet, gameObject);
      }
   }
}
