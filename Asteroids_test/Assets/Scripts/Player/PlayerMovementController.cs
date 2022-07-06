using System;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovementController : MonoBehaviour
{
   [SerializeField, Range(0.1f, 2f)] private float acceleration;
   [SerializeField, Range(0.5f, 2f)] private float maxSpeed;
   [SerializeField, Range(0, 1f)] private float inertiaDecayCoefficient; // 0 - without inertia decay, 1 - almost without inertia
   [Space]
   [SerializeField, Range(1.5f,10f)] private float mouseRotationSpeed;
   [SerializeField, Range(50f, 200f)] private float keyboardRotationSpeed; // degrees per second

   // private static variables
   private static Vector3 _movementVector = Vector3.zero;
   private static GameObject _player;

   private void Awake()
   {
      _player = GameObject.FindWithTag("Player");
   }

   private void Update()
   {
      if (PlayerData.IsOnlyKeyboard)
      {
         transform.Rotate(Vector3.forward, keyboardRotationSpeed * -InputController._movementVector.y * Time.deltaTime );
      }
      else
      {
         // look at mouse
         Vector3 direction = InputController._mouseWorldPos - transform.position;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), mouseRotationSpeed * Time.deltaTime);
      }
      
      // inertia decay
      _movementVector = Vector3.Slerp(_movementVector, Vector3.zero, inertiaDecayCoefficient * Time.deltaTime);
      
      // move
      if (InputController._movementVector.x != 0)
      {
         _movementVector += transform.right * (acceleration * Time.deltaTime);
         SoundsManager.PlaySound(Constants.Sounds.Thruster);
      }
      
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

   public static void ResetValues()
   {
      _player.transform.position = Vector3.zero;
      _movementVector = Vector3.zero;
   }
}