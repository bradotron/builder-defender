using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  public static MusicManager Instance { get; private set; }
  private AudioSource audioSource;

  private void Awake()
  {
    Instance = this;
    audioSource = GetComponent<AudioSource>();
    audioSource.volume = 0.5f;
  }

  public void IncreaseVolume()
  {
    audioSource.volume = Mathf.Clamp(audioSource.volume + 0.1f, 0f, 1f);
  }

  public void DecreaseVolume()
  {
    audioSource.volume = Mathf.Clamp(audioSource.volume - 0.1f, 0f, 1f);
  }

  public float getVolumeScale()
  {
    return audioSource.volume;
  }
}
