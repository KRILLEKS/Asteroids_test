using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static bool _isGameStarted = false;
   
   private void Awake()
   {
      Time.timeScale = 0;
   }

   public void PauseGame()
   {
      // already paused
      if (Time.timeScale == 0)
      {
         ResumeGame();
         return;
      }
      
      Time.timeScale = 0;
      PauseMenuController.OpenPauseMenu();
   }

   public void ResumeGame()
   {
      if (_isGameStarted == false)
         return;
      
      Time.timeScale = 1;
      PauseMenuController.ClosePauseMenu();
   }

   public void ExitGame()
   {
      Application.Quit();
   }

   public void StartNewGame()
   {
      PlayerData.ResetValues();
      InGameUIController.ResetValues();
      PlayerMovementController.ResetValues();
      ObjectPooler.ResetValues();
      SpawnHandler.ResetValues();

      _isGameStarted = true;
      
      ResumeGame();
   }
}
