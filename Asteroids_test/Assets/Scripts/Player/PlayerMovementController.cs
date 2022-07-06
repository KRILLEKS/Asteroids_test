using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovementController : MonoBehaviour
{
   [SerializeField, Range(0.1f, 1f)] private float acceleration;
   [SerializeField, Range(0.5f, 2f)] private float maxSpeed;
   [SerializeField, Range(0, 1f)] private float inertiaDecayCoefficient; // 0 - without inertia decay, 1 - almost without inertia
   [Space]
   [SerializeField, Range(1.5f,10f)] private float rotationSpeed;

   // private static variables
   private static Vector3 _movementVector = Vector3.zero;

   private void Update()
   {
      // look at mouse
      Vector3 direction = InputController._mouseWorldPos - transform.position;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);

      // inertia decay
      _movementVector = Vector3.Slerp(_movementVector, Vector3.zero, inertiaDecayCoefficient * Time.deltaTime);
      
      // move
      if (InputController._movementVector.x != 0)
         _movementVector += transform.right * (acceleration * Time.deltaTime);
      
      // clamp
      if (Mathf.Abs(_movementVector.x) > maxSpeed)
         _movementVector.x = Mathf.Clamp(_movementVector.x,-maxSpeed, maxSpeed);
      if (Mathf.Abs(_movementVector.y) > maxSpeed)
         _movementVector.y = Mathf.Clamp(_movementVector.y,-maxSpeed, maxSpeed);

      transform.position += _movementVector * Time.deltaTime;

      if (BordersHandler.IsWithinBorders(transform.position) == false)
         transform.position = BordersHandler.GetPosWithinBorders(transform.position);
   }

   public static void StopMovement()
   {
      _movementVector = Vector3.zero;
   }
}