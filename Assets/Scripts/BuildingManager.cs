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
      if (activeBuildingType != null)
      {
        if (CanSpawnBuilding(activeBuildingType, Utils.GetMouseWorldPosition(), out string canSpawnError))
        {
          if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCosts, out string canAffordError))
          {
            ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCosts);
            Instantiate(activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
          }
          else
          {
            TooltipUI.Instance.Show(canAffordError, new TooltipUI.TooltipTimer() { timer = 2f });
          }
        }
        else
        {
          TooltipUI.Instance.Show(canSpawnError, new TooltipUI.TooltipTimer() { timer = 2f });
        }
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

  private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
  {
    BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

    // area must be clear
    bool isAreaClear = collider2Ds.Length == 0;
    if (!isAreaClear)
    {
      errorMessage = "Construction is blocked!";
      return false;
    }

    // must not be any of the same buildingType within the min radius
    collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
    foreach (Collider2D collider2D in collider2Ds)
    {
      BuildingTypeReference buildingTypeReference = collider2D.GetComponent<BuildingTypeReference>();
      if (buildingTypeReference != null && buildingTypeReference.buildingType == buildingType)
      {
        errorMessage = "Too close to another " + buildingType.sName;
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
        errorMessage = "";
        return true;
      }
    }
    errorMessage = "Too far from buildings!";
    return false;
  }
}
