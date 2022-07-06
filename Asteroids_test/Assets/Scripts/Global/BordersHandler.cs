using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BordersHandler : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   [FormerlySerializedAs("offset"), SerializeField]
   private float offsetSerializable;

   // public static variables
   // this are corners of a border not of camera (camera corner + offset)
   public static Vector2 BotLeftCorner;
   public static Vector2 TopRightCorner;

   // private static variables
   private static float offset;

   private void Awake()
   {
      offset = offsetSerializable;

      BotLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane))
                      - new Vector3(offset, offset, 0);
      TopRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane))
                       + new Vector3(offset, offset, 0);
   }

   public static bool IsWithinBorders(Vector3 position)
   {
      return position.x > BotLeftCorner.x && position.x < TopRightCorner.x &&
             position.y < TopRightCorner.y && position.y > BotLeftCorner.y;
   }

   public static Vector3 GetPosWithinBorders(Vector3 position)
   {
      if (position.x < BotLeftCorner.x || position.x > TopRightCorner.x)
         return new Vector3(-position.x, position.y);
      else if (position.y < BotLeftCorner.y || position.y > TopRightCorner.y)
         return new Vector3(position.x, -position.y);
      else
         return position;
   }

   public static Vector3 GetEdgePosition()
   {
      switch (Random.Range(0, 4))
      {
         case 1: // bot
            return new Vector3(Random.Range(BotLeftCorner.x, TopRightCorner.x), BotLeftCorner.y);
         case 2: // top
            return new Vector3(Random.Range(BotLeftCorner.x, TopRightCorner.x), TopRightCorner.y);
         case 3: // left
            return new Vector3(BotLeftCorner.x, Random.Range(BotLeftCorner.y, BotLeftCorner.y));
         default: // right
            return new Vector3(BotLeftCorner.x, Random.Range(BotLeftCorner.y, TopRightCorner.y));
      }
   }

   public static Vector3 GetRandomPosition()
   {
      return new Vector3(Random.Range(BotLeftCorner.x + offset, TopRightCorner.x - offset), Random.Range(BotLeftCorner.y - offset, TopRightCorner.y + offset));
   }

   public static Vector3 GetRandomUFOPosition()
   {
      return new Vector3(BotLeftCorner.x, Random.Range((BotLeftCorner.y + offset) * (1 - Constants.OffsetFromBorder / 100),
                                                       (TopRightCorner.y - offset) * (1 - Constants.OffsetFromBorder / 100)));
   }

   private void OnDrawGizmos()
   {
      if (Application.isPlaying)
         return;

      BotLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane))
                      - new Vector3(offsetSerializable, offsetSerializable, 0);
      TopRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane))
                       + new Vector3(offsetSerializable, offsetSerializable, 0);

      Gizmos.DrawLine(BotLeftCorner, new Vector3(BotLeftCorner.x, TopRightCorner.y)); // left
      Gizmos.DrawLine(TopRightCorner, new Vector3(TopRightCorner.x, BotLeftCorner.y)); // right
      Gizmos.DrawLine(TopRightCorner, new Vector3(BotLeftCorner.x, TopRightCorner.y)); // top
      Gizmos.DrawLine(BotLeftCorner, new Vector3(TopRightCorner.x, BotLeftCorner.y)); // bot
   }
}