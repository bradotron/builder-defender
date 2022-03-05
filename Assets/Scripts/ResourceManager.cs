using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  public static ResourceManager Instance { get; private set; }
  private Dictionary<ResourceTypeSO, int> resourceAmounts;

  public event EventHandler OnResourceAmountChanged;

  private void Awake()
  {
    Instance = this;

    resourceAmounts = new Dictionary<ResourceTypeSO, int>();
    ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
    foreach (ResourceTypeSO resourceType in resourceTypeList.list)
    {
      resourceAmounts.Add(resourceType, 0);
    }
  }

  private void Start()
  {
    // Test_LogResourceAmounts();
  }

  private void Update()
  {
  }

  public void AddResource(ResourceTypeSO resourceType, int amount)
  {
    resourceAmounts[resourceType] += amount;
    OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
  }

  public int GetResourceAmount(ResourceTypeSO resourceType)
  {
    return resourceAmounts[resourceType];
  }

  private void Test_LogResourceAmounts()
  {
    foreach (ResourceTypeSO resourceType in resourceAmounts.Keys)
    {
      Debug.Log(resourceType.sName + ": " + resourceAmounts[resourceType]);
    }
  }

  public bool CanAfford(ResourceAmount[] resourceCosts, out string errorMessage)
  {
    foreach (ResourceAmount resourceAmount in resourceCosts)
    {
      if(GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount ) {
        errorMessage = "Insufficient " + resourceAmount.resourceType.sName;
        return false;
      }
    }
    errorMessage = "";
    return true;
  }

  public void SpendResources(ResourceAmount[] resourceCosts)
  {
    foreach (ResourceAmount resourceAmount in resourceCosts)
    {
      this.AddResource(resourceAmount.resourceType, -resourceAmount.amount);
    }
  }
}
