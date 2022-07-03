using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersHandler : MonoBehaviour
{
   [SerializeField] private Camera camera;
   [SerializeField] private float offset;

   // private variables
   // this are corners of a border not of camera (camera corner + offset)
   private Vector2 _botLeftCorner;
   private Vector2 _topRightCorner;

   private void Awake()
   {
      _botLeftCorner = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane))
                       - new Vector3(offset, offset,0);
      _topRightCorner = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane))
                        + new Vector3(offset, offset, 0);
   }

   private void OnDrawGizmos()
   {
      if (Application.isPlaying)
         return;

      _botLeftCorner = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane))
                       - new Vector3(offset, offset,0);
      _topRightCorner = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane))
                        + new Vector3(offset, offset, 0);

      Gizmos.DrawLine(_botLeftCorner, new Vector3(_botLeftCorner.x, _topRightCorner.y)); // left
      Gizmos.DrawLine(_topRightCorner, new Vector3(_topRightCorner.x, _botLeftCorner.y)); // right
      Gizmos.DrawLine(_topRightCorner, new Vector3(_botLeftCorner.x, _topRightCorner.y)); // top
      Gizmos.DrawLine(_botLeftCorner, new Vector3(_topRightCorner.x, _botLeftCorner.y)); // bot
   }
}