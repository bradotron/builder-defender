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
  private float volumeScale = 0.5f; // goes from 0 to 1
  private void Awake()
  {
    Instance = this;

    audioSource = GetComponent<AudioSource>();

    audioClips = new Dictionary<SoundName, AudioClip>();
    loadAudioClips();
  }
  public void PlaySound(SoundName sound)
  {
    audioSource.PlayOneShot(audioClips[sound], volumeScale);
  }

  private void loadAudioClips()
  {
    foreach (SoundName soundName in Enum.GetValues(typeof(SoundName)))
    {
      audioClips.Add(soundName, Resources.Load<AudioClip>(soundName.ToString()));
    }
  }

  public void IncreaseVolume()
  {
    volumeScale = Mathf.Clamp(volumeScale + 0.1f, 0f, 1f);
  }

  public void DecreaseVolume()
  {
    volumeScale = Mathf.Clamp(volumeScale - 0.1f, 0f, 1f);
  }

  public float getVolumeScale() {
    return volumeScale;
  }
}
