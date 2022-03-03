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
    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
    {
      if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, Utils.GetMouseWorldPosition()))
      {
        Instantiate(activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
      }
    }
  }

  public void SetActiveBuildingType(BuildingTypeSO buildingType)
  {
    activeBuildingType = buildingType;
    OnActiveBuildingTypeChangedEventArgs eventArgs = new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType };
    OnActiveBuildingTypeChanged?.Invoke(this, eventArgs);
  }

  public BuildingTypeSO GetActiveBuildingType()
  {
    return activeBuildingType;
  }

  private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
  {
    BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

    // area must be clear
    bool isAreaClear = collider2Ds.Length == 0;
    if (!isAreaClear)
    {
      return false;
    }

    // must not be any of the same buildingType within the min radius
    collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
    foreach (Collider2D collider2D in collider2Ds)
    {
      BuildingTypeReference buildingTypeReference = collider2D.GetComponent<BuildingTypeReference>();
      if (buildingTypeReference != null && buildingTypeReference.buildingType == buildingType)
      {
        return false;
      }
    }

    // can't build too far from other buildings
    float maxConstructionRadius = 25f;
    collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
    foreach (Collider2D collider2D in collider2Ds)
    {
      BuildingTypeReference buildingTypeReference = collider2D.GetComponent<BuildingTypeReference>();
      if (buildingTypeReference != null)
      {
        return true;
      }
    }

    return false;
  }
}
