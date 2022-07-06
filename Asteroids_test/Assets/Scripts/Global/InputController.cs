using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;
   [SerializeField] private UnityEvent onLeftClick;
   [SerializeField] private UnityEvent onSpaceClick;
   [SerializeField] private UnityEvent onEscClick;
   
   // public static variables
   public static Vector3 _mouseWorldPos;
   public static Vector3 _movementVector;

   private void Awake()
   {
      onLeftClick ??= new UnityEvent();
      onEscClick ??= new UnityEvent();
      onSpaceClick ??= new UnityEvent();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Mouse0))
         onLeftClick.Invoke();
      
      if (Input.GetKeyDown(KeyCode.Space))
         onSpaceClick.Invoke();
      
      if (Input.GetKeyDown(KeyCode.Escape))
         onEscClick.Invoke();
      
      _mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      
      _movementVector.y = Input.GetAxisRaw("Horizontal");
      _movementVector.x = Input.GetAxisRaw("Vertical");

      // right mouse acceleration
      if (Input.GetKey(KeyCode.Mouse1))
         _movementVector.x = 1;
   }
}
