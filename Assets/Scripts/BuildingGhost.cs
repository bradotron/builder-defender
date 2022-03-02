using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
  private GameObject spriteGameObject;
  private void Awake()
  {
    spriteGameObject = spriteGameObject ?? transform.Find("sprite").gameObject;

    Hide();
  }

  private void Start()
  {
    BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
  }

  private void Update()
  {
    transform.position = Utils.GetMouseWorldPosition();
  }

  private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
  {
    if (e.activeBuildingType == null)
    {
      Hide();
    }
    else
    {
      Show(e.activeBuildingType.sprite);
    }
  }

  private void Show(Sprite ghostSprite)
  {
    spriteGameObject.SetActive(true);

    spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
  }

  private void Hide()
  {
    spriteGameObject.SetActive(false);
  }
}
