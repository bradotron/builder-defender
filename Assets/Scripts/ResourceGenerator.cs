using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
  private BuildingTypeSO buildingType;
  private float generatorCooldown;
  private float timeSinceGeneration = 0f;

  private void Awake()
  {
    buildingType = GetComponent<BuildingTypeReference>().buildingType;
    generatorCooldown = buildingType.resourceGeneratorData.generatorCooldown;
  }

  // Update is called once per frame
  void Update()
  {
    timeSinceGeneration += Time.deltaTime;
    if (timeSinceGeneration >= generatorCooldown)
    {
      timeSinceGeneration = 0f;
      ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
    }
  }
}
