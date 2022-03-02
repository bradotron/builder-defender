using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
  private Dictionary<BuildingTypeSO, Transform> buildingTypeTransforms;
  private BuildingTypeListSO buildingTypeList;
  private BuildingTypeSO buildingType;

  [SerializeField] private Sprite cursorSprite;
  private Transform cursorBtn;

  private float offsetAmount = 130f;
  private void Awake()
  {
    buildingTypeTransforms = new Dictionary<BuildingTypeSO, Transform>();

    Transform btnTemplate = transform.Find("btnTemplate");
    btnTemplate.gameObject.SetActive(false);

    buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

    int i = 0;

    cursorBtn = Instantiate(btnTemplate, transform);
    cursorBtn.gameObject.SetActive(true);

    cursorBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * i, 0);
    cursorBtn.Find("image").GetComponent<Image>().sprite = cursorSprite;
    cursorBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -50f);
    cursorBtn.GetComponent<Button>().onClick.AddListener(() =>
    {
      BuildingManager.Instance.SetActiveBuildingType(null);
    });

    i++;
    foreach (BuildingTypeSO buildingType in buildingTypeList.list)
    {
      Transform btnTransform = Instantiate(btnTemplate, transform);
      btnTransform.gameObject.SetActive(true);

      btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * i, 0);
      btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
      btnTransform.GetComponent<Button>().onClick.AddListener(() =>
      {
        BuildingManager.Instance.SetActiveBuildingType(buildingType);
      });

      buildingTypeTransforms.Add(buildingType, btnTransform);

      i++;
    }
  }

  private void Start()
  {
    BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    UpdateActiveBuildingType();
  }

  private void BuildingManager_OnActiveBuildingTypeChanged(object sender, EventArgs e)
  {
    UpdateActiveBuildingType();
  }
  public void UpdateActiveBuildingType()
  {
    BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

    cursorBtn.Find("selected").gameObject.SetActive(activeBuildingType == null);

    foreach (BuildingTypeSO key in buildingTypeTransforms.Keys)
    {
      buildingTypeTransforms[key].Find("selected").gameObject.SetActive(activeBuildingType == key);
    }
  }
}
