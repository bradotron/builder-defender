using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  private Dictionary<ResourceTypeSO, int> resourceAmounts;

  private void Awake()
  {
    resourceAmounts = new Dictionary<ResourceTypeSO, int>();
    ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
    foreach (ResourceTypeSO resourceType in resourceTypeList.list)
    {
      resourceAmounts.Add(resourceType, 0);
    }
  }

  private void Start()
  {
    Test_LogResourceAmounts();
  }

  private void Update()
  {
  }

  public void AddResource(ResourceTypeSO resourceType, int amount)
  {
    resourceAmounts[resourceType] += amount;
  }

  private void Test_LogResourceAmounts()
  {
    foreach (ResourceTypeSO resourceType in resourceAmounts.Keys)
    {
      Debug.Log(resourceType.sName + ": " + resourceAmounts[resourceType]);
    }
  }
}
