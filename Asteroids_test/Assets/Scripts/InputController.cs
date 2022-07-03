using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   
   // public static variables
   public static Vector3 _mouseWorldPos;
   public static Vector3 _movementVector;
   
   private void Update()
   {
      _mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      
      _movementVector.y = Input.GetAxisRaw("Horizontal");
      _movementVector.x = Input.GetAxisRaw("Vertical");
   }
}
