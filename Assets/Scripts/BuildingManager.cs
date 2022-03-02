using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
  public static BuildingManager Instance { get; private set; }
  private BuildingTypeListSO buildingTypeList;
  private BuildingTypeSO activeBuildingType;
  public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

  public class OnActiveBuildingTypeChangedEventArgs : EventArgs
  {
    public BuildingTypeSO activeBuildingType;
  }

  private void Awake()
  {
    Instance = this;

    buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
    {
      Instantiate(activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
    }
  }

  public void SetActiveBuildingType(BuildingTypeSO buildingType)
  {
    activeBuildingType = buildingType;
    OnActiveBuildingTypeChangedEventArgs eventArgs =  new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType };
    OnActiveBuildingTypeChanged?.Invoke(this, eventArgs);
  }

  public BuildingTypeSO GetActiveBuildingType()
  {
    return activeBuildingType;
  }
}
