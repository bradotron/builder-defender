using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance { get; private set; }
  public enum SoundName
  {
    BuildingDamaged,
    BuildingDestroyed,
    BuildingPlaced,
    EnemyDie,
    EnemyHit,
    EnemyWaveStarting,
    GameOver,
  }

  private Dictionary<SoundName, AudioClip> audioClips;
  private AudioSource audioSource;
  private void Awake()
  {
    Instance = this;

    audioSource = GetComponent<AudioSource>();

    audioClips = new Dictionary<SoundName, AudioClip>();
    loadAudioClips();
  }
  public void PlaySound(SoundName sound)
  {
    audioSource.PlayOneShot(audioClips[sound]);
  }

  private void loadAudioClips()
  {
    foreach (SoundName soundName in Enum.GetValues(typeof(SoundName)))
    {
      audioClips.Add(soundName, Resources.Load<AudioClip>(soundName.ToString()));
    }
  }
}
