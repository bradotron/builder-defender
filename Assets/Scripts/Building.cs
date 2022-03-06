using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
  private BuildingTypeSO buildingType;
  private HealthSystem healthSystem;


  void Start()
  {
    buildingType = GetComponent<BuildingTypeReference>().buildingType;
    healthSystem = GetComponent<HealthSystem>();
    healthSystem.SetMaxHealth(buildingType.maxHealthAmount, true);
    healthSystem.OnDied += HealthSystem_OnDied;

    Debug.Log(healthSystem.GetCurrentHealth());
  }

  private void HealthSystem_OnDied(object sender, EventArgs e)
  {
    Destroy(gameObject);
  }
}
