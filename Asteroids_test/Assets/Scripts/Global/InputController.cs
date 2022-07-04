using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   [SerializeField] private UnityEvent onLeftClick;
   
   // public static variables
   public static Vector3 _mouseWorldPos;
   public static Vector3 _movementVector;

   private void Awake()
   {
      onLeftClick ??= new UnityEvent();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Mouse0))
         onLeftClick.Invoke();
      
      _mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      
      _movementVector.y = Input.GetAxisRaw("Horizontal");
      _movementVector.x = Input.GetAxisRaw("Vertical");
   }
}
