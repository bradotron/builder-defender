using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
  private float offsetAmount = -160f;
  private ResourceTypeListSO resourceTypeList;
  private Dictionary<ResourceTypeSO, Transform> resourceTypeTransforms;
  private void Awake()
  {
    resourceTypeTransforms = new Dictionary<ResourceTypeSO, Transform>();

    resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

    Transform resourceTemplate = transform.Find("resourceTemplate");
    resourceTemplate.gameObject.SetActive(false);

    for (int i = 0; i < resourceTypeList.list.Count; i++)
    {
      ResourceTypeSO resourceType = resourceTypeList.list[i];
      Transform resourceTransform = Instantiate(resourceTemplate, transform);

      resourceTransform.gameObject.SetActive(true);

      resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * i, 0);
      resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;
      resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText("0");

      resourceTypeTransforms.Add(resourceType, resourceTransform);
    }
  }
  // Start is called before the first frame update
  void Start()
  {
    ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
    UpdateResourceAmounts();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
  {
    UpdateResourceAmounts();
  }

  private void UpdateResourceAmounts()
  {
    foreach (ResourceTypeSO resourceType in resourceTypeList.list)
    {
      Transform resourceTransform = resourceTypeTransforms[resourceType];
      resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(ResourceManager.Instance.GetResourceAmount(resourceType).ToString());
    }
  }
}
