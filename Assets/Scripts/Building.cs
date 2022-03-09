using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
  private BuildingTypeSO buildingType;
  private HealthSystem healthSystem;
  private Transform buildingDemolishButton;

  private void Awake()
  {
    buildingDemolishButton = transform.Find("pf_BuildingDemolishButton");
    HideBuildingDemolishButton();
  }

  void Start()
  {
    buildingType = GetComponent<BuildingTypeReference>().buildingType;
    healthSystem = GetComponent<HealthSystem>();
    healthSystem.SetMaxHealth(buildingType.maxHealthAmount, true);
    healthSystem.OnDied += HealthSystem_OnDied;
  }

  private void HealthSystem_OnDied(object sender, EventArgs e)
  {
    Destroy(gameObject);
  }

  private void OnMouseEnter()
  {
    ShowBuildingDemolishButton();
  }

  private void OnMouseExit()
  {
    HideBuildingDemolishButton();
  }

  private void ShowBuildingDemolishButton() {
    if(buildingDemolishButton != null) {
      buildingDemolishButton.gameObject.SetActive(true);
    }
  }
  private void HideBuildingDemolishButton() {
    if(buildingDemolishButton != null) {
      buildingDemolishButton.gameObject.SetActive(false);
    }
  }
}
