using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
  private BuildingTypeSO buildingType;
  private HealthSystem healthSystem;
  private Transform buildingDemolishButton;
  private Transform buildingRepairButton;

  private void Awake()
  {
    buildingDemolishButton = transform.Find("pf_BuildingDemolishButton");
    buildingRepairButton = transform.Find("pf_BuildingRepairButton");
    HideBuildingDemolishButton();
    HideBuildingRepairButton();
  }

  void Start()
  {
    buildingType = GetComponent<BuildingTypeReference>().buildingType;
    healthSystem = GetComponent<HealthSystem>();
    healthSystem.SetMaxHealth(buildingType.maxHealthAmount, true);
    healthSystem.OnDied += HealthSystem_OnDied;
    healthSystem.OnDamaged += HealthSystem_OnDamaged;
    healthSystem.OnHealed += HealthSystem_OnHealed;
  }

  private void HealthSystem_OnDamaged(object sender, EventArgs e)
  {
    ShowBuildingRepairButton();
  }
  
  private void HealthSystem_OnHealed(object sender, EventArgs e)
  {
    if (healthSystem.IsFullHealth())
    {
      HideBuildingRepairButton();
    }
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

  private void ShowBuildingDemolishButton()
  {
    if (buildingDemolishButton != null)
    {
      buildingDemolishButton.gameObject.SetActive(true);
    }
  }
  private void HideBuildingDemolishButton()
  {
    if (buildingDemolishButton != null)
    {
      buildingDemolishButton.gameObject.SetActive(false);
    }
  }
  private void ShowBuildingRepairButton()
  {
    if (buildingRepairButton != null)
    {
      buildingRepairButton.gameObject.SetActive(true);
    }
  }
  private void HideBuildingRepairButton()
  {
    if (buildingRepairButton != null)
    {
      buildingRepairButton.gameObject.SetActive(false);
    }
  }
}
