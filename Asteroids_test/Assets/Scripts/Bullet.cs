using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   // internal variables
   internal float _distanceTraveled;
   internal bool _needs2Reset = true;
   internal Vector3 _previousPos;
   
   internal void CheckState()
   {
      CheckForReset();
      
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
      else
      {
         _distanceTraveled += Vector3.Distance(transform.position, _previousPos);
         _previousPos = transform.position;
      }
   }

   internal virtual void CheckForReset()
   {
      // set start pos
      if (_needs2Reset)
      {
         _previousPos = transform.position;
         _needs2Reset = false;
         _distanceTraveled = 0;
      }
   }
}