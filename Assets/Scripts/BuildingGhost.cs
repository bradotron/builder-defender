using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
  private GameObject spriteGameObject;
  private ResourceNodesNearbyOverlay resourceNodesNearbyOverlay;
  private void Awake()
  {
    spriteGameObject = spriteGameObject ?? transform.Find("sprite").gameObject;
    resourceNodesNearbyOverlay = transform.Find("pf_ResourceNodesNearbyOverlay").GetComponent<ResourceNodesNearbyOverlay>();
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
      resourceNodesNearbyOverlay.Hide();
      Hide();
    }
    else
    {
      Show(e.activeBuildingType.sprite);
      if (e.activeBuildingType.resourceGeneratorData.resourceType != null)
      {
        resourceNodesNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
      }
      else
      {
        resourceNodesNearbyOverlay.Hide();
      }
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
