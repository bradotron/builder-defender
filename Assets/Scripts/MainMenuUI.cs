using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  private void Awake()
  {
    transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(playBtn_OnClick);
    transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(quitBtn_OnClick);
  }

  private void playBtn_OnClick()
  {
    GameSceneManager.Load(GameSceneManager.Scene.GameScene);
  }

  private void quitBtn_OnClick()
  {
    Application.Quit();
  }
}
