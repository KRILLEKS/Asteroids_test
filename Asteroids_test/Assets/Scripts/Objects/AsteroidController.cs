using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
   // private variable 
   private Vector3 moveVector;

   private void Awake()
   {
      var sprites = Resources.LoadAll<Sprite>($"AsteroidTextures/");
      GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
   }

   private void Update()
   {
      transform.position += moveVector * (Time.deltaTime * Constants.AsteroidSpeed);

      if (BordersHandler.IsWithinBorders(transform.position) == false)
         transform.position = BordersHandler.GetEdgePosition();
   }

   public void GenerateMoveVector()
   {
      moveVector.x = Random.Range(0,2) == 0 ? Random.Range(-1f, -0.4f) : Random.Range(0.4f,1f); // asteroids shouldn't be too slow
      moveVector.y = Random.Range(-0.5f, 0.5f);
   }
}
