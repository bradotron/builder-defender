using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
  private BuildingTypeListSO buildingTypeList;
  private BuildingTypeSO buildingType;
  private Camera mainCamera;

  private void Awake()
  {
    buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    buildingType = buildingTypeList.list[0];

  }
  // Start is called before the first frame update
  private void Start()
  {
    mainCamera = Camera.main;
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
    }

    if (Input.GetKeyDown(KeyCode.T))
    {
      buildingType = buildingTypeList.list[0];
    }
    if (Input.GetKeyDown(KeyCode.Y))
    {
      buildingType = buildingTypeList.list[1];
    }
  }

  private Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }
}
