using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BordersHandler : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   [SerializeField] private float offset;

   // private static variables
   // this are corners of a border not of camera (camera corner + offset)
   private static Vector2 _botLeftCorner;
   private static Vector2 _topRightCorner;

   private void Awake()
   {
      _botLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane))
                       - new Vector3(offset, offset,0);
      _topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane))
                        + new Vector3(offset, offset, 0);
   }

   public static bool IsWithinBorders(Vector3 position)
   {
      return position.x > _botLeftCorner.x && position.x < _topRightCorner.x &&
             position.y < _topRightCorner.y && position.y > _botLeftCorner.y;
   }

   public static Vector3 GetPosWithinBorders(Vector3 position)
   {
      if (position.x < _botLeftCorner.x || position.x > _topRightCorner.x)
         return new Vector3(-position.x, position.y);
      else if (position.y < _botLeftCorner.y || position.y > _topRightCorner.y)
         return new Vector3(position.x, -position.y);
      else
         return position;
   }

   public static Vector3 GetEdgePosition()
   {
      switch (Random.Range(0,4))
      {
         case 1: // bot
            return new Vector3(Random.Range(_botLeftCorner.x, _topRightCorner.x), _botLeftCorner.y);
         case 2: // top
            return new Vector3(Random.Range(_botLeftCorner.x, _topRightCorner.x), _topRightCorner.y);
         case 3: // left
            return new Vector3(_botLeftCorner.x,Random.Range(_botLeftCorner.y, _botLeftCorner.y));
         default: // right
            return new Vector3(_botLeftCorner.x,Random.Range(_botLeftCorner.y, _topRightCorner.y));
      }
   }

   private void OnDrawGizmos()
   {
      if (Application.isPlaying)
         return;

      _botLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane))
                       - new Vector3(offset, offset,0);
      _topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane))
                        + new Vector3(offset, offset, 0);

      Gizmos.DrawLine(_botLeftCorner, new Vector3(_botLeftCorner.x, _topRightCorner.y)); // left
      Gizmos.DrawLine(_topRightCorner, new Vector3(_topRightCorner.x, _botLeftCorner.y)); // right
      Gizmos.DrawLine(_topRightCorner, new Vector3(_botLeftCorner.x, _topRightCorner.y)); // top
      Gizmos.DrawLine(_botLeftCorner, new Vector3(_topRightCorner.x, _botLeftCorner.y)); // bot
   }
}