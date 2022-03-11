using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
  [SerializeField] private HealthSystem healthSystem;
  [SerializeField] private ResourceTypeSO goldResourceType;
  private void Awake()
  {
    transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
    {
      int missingHealth = healthSystem.GetMaxHealth() - healthSystem.GetCurrentHealth();
      ResourceAmount repairResourceAmount = new ResourceAmount { resourceType = goldResourceType, amount = missingHealth / 2 };
      ResourceAmount[] repairResourceAmounts = new ResourceAmount[] { repairResourceAmount };
      if (ResourceManager.Instance.CanAfford(repairResourceAmounts, out string errorMessage))
      {
        ResourceManager.Instance.SpendResources(repairResourceAmounts);
        healthSystem.HealFull();
      }
      else
      {
        TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
      }
    });
  }

}
