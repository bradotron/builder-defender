using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
  // [SerializeField] private 
  private TextMeshProUGUI currentSoundText;
  private TextMeshProUGUI currentMusicText;

  private void Awake()
  {
    transform.Find("soundPlusBtn").GetComponent<Button>().onClick.AddListener(soundPlusBtn_OnClick);
    transform.Find("soundMinusBtn").GetComponent<Button>().onClick.AddListener(soundMinusBtn_OnClick);
    currentSoundText = transform.Find("currentSoundText").GetComponent<TextMeshProUGUI>();

    transform.Find("musicPlusBtn").GetComponent<Button>().onClick.AddListener(musicPlusBtn_OnClick);
    transform.Find("musicMinusBtn").GetComponent<Button>().onClick.AddListener(musicMinusBtn_OnClick);
    currentMusicText = transform.Find("currentMusicText").GetComponent<TextMeshProUGUI>();

    transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(mainMenuBtn_OnClick);

    gameObject.SetActive(false);
  }

  private void Start()
  {
    updateCurrentSoundText();
    updateCurrentMusicText();
  }

  private void soundPlusBtn_OnClick()
  {
    SoundManager.Instance.IncreaseVolume();
    updateCurrentSoundText();
  }
  private void soundMinusBtn_OnClick()
  {
    SoundManager.Instance.DecreaseVolume();
    updateCurrentSoundText();
  }
  private void musicPlusBtn_OnClick()
  {
    MusicManager.Instance.IncreaseVolume();
    updateCurrentMusicText();
  }
  private void musicMinusBtn_OnClick()
  {
    MusicManager.Instance.DecreaseVolume();
    updateCurrentMusicText();
  }

  private void mainMenuBtn_OnClick()
  {
    Time.timeScale = 1f;
    
    GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
  }

  private void updateCurrentSoundText()
  {
    int soundManagerVolume = Mathf.RoundToInt(SoundManager.Instance.getVolumeScale() * 10);
    currentSoundText.SetText(soundManagerVolume.ToString());
  }

  private void updateCurrentMusicText()
  {
    int musicManagerVolume = Mathf.RoundToInt(MusicManager.Instance.getVolumeScale() * 10);
    currentMusicText.SetText(musicManagerVolume.ToString());
  }

  public void ToggleVisible()
  {
    gameObject.SetActive(!gameObject.activeSelf);

    Time.timeScale = gameObject.activeSelf ? 0f : 1f;
  }
}
