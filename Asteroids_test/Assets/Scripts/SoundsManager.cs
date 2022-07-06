using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// I put everything in here
// I'm not sure that this is right
public class SoundsManager : MonoBehaviour
{
   [SerializeField] private AudioClip playerShootSoundSerializable;
   [SerializeField] private AudioClip UFOSoundSerializable;
   [SerializeField] private AudioClip thrusterSoundSerializable;
   [SerializeField] private AudioClip smallExplosionSoundSerializable;
   [SerializeField] private AudioClip mediumExplosionSoundSerializable;
   [SerializeField] private AudioClip bigExplosionSoundSerializable;

   // private static variables
   private static Dictionary<Constants.Sounds, AudioSource> _soundsDictionary = new Dictionary<Constants.Sounds, AudioSource>();

   private void Awake()
   {
      AddSource(Constants.Sounds.PlayerFire, playerShootSoundSerializable);
      AddSource(Constants.Sounds.UFO, UFOSoundSerializable);
      AddSource(Constants.Sounds.Thruster, thrusterSoundSerializable);
      AddSource(Constants.Sounds.SmallExplosion, smallExplosionSoundSerializable);
      AddSource(Constants.Sounds.MediumExplosion, mediumExplosionSoundSerializable);
      AddSource(Constants.Sounds.BigExplosion, bigExplosionSoundSerializable);

      void AddSource(Constants.Sounds sound, AudioClip clip)
      {
         var source = gameObject.AddComponent<AudioSource>();
         source.clip = clip;
         _soundsDictionary.Add(sound, source);
      }
   }

   public static void PlaySound(Constants.Sounds sound)
   {
      if (_soundsDictionary[sound].isPlaying == false)
         _soundsDictionary[sound].Play();
   }
}