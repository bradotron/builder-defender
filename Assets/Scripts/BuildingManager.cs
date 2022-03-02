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
  public event EventHandler OnActiveBuildingTypeChanged;
  private Camera mainCamera;

  private void Awake()
  {
    Instance = this;

    buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
  }
  // Start is called before the first frame acupdate
  private void Start()
  {
    mainCamera = Camera.main;
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetMouseButtonDown(0) && activeBuildingType != null && !EventSystem.current.IsPointerOverGameObject())
    {
      Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
    }
  }

  private Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }

  public void SetActiveBuildingType(BuildingTypeSO buildingType)
  {
    activeBuildingType = buildingType;
    OnActiveBuildingTypeChanged?.Invoke(this, EventArgs.Empty);
  }

  public BuildingTypeSO GetActiveBuildingType()
  {
    return activeBuildingType;
  }
}
