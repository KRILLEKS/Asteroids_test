using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
   [SerializeField] private GameObject pauseMenuSerializable;
   [SerializeField] private Toggle toggleScheme;

   // private static variables
   private static GameObject _pauseMenu;
   private static RawImage _blurRawImage;
   private static PauseMenuController _pauseMenuController;
   private static float _blurStartTime;

   private void Awake()
   {
      _pauseMenu = pauseMenuSerializable;
      _blurRawImage = _pauseMenu.transform.Find("Blur").GetComponent<RawImage>();
      _pauseMenuController = this;
      
      _pauseMenu.SetActive(true);
   }

   public static void OpenPauseMenu()
   {
      _pauseMenu.SetActive(true);
      _blurRawImage.color = new Color(_blurRawImage.color.r,
                                      _blurRawImage.color.g,
                                      _blurRawImage.color.b,
                                      0);

      _pauseMenuController.StartCoroutine(BlurCoroutine());
   }

   private static IEnumerator BlurCoroutine()
   {
      _blurStartTime = Time.realtimeSinceStartup;

      while (_blurRawImage.color.a < 1)
      {
         _blurRawImage.color = new Color(_blurRawImage.color.r,
                                         _blurRawImage.color.g,
                                         _blurRawImage.color.b,
                                         Mathf.Lerp(0, 1, (Time.realtimeSinceStartup - _blurStartTime) / Constants.PauseMenuOpenSpeed));

         yield return null;
      }
   }

   public static void ClosePauseMenu()
   {
      _pauseMenu.SetActive(false);
   }

   // invokes on toggle value change
   public void ChangeControlScheme()
   {
      PlayerData.IsOnlyKeyboard = toggleScheme.isOn == false;
   }
}