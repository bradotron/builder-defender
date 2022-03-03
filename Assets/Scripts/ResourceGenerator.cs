using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
  private ResourceGeneratorData resourceGeneratorData;
  private float timeSinceGeneration = 0f;
  private float generatorCooldown;

  private void Awake()
  {
    resourceGeneratorData = GetComponent<BuildingTypeReference>().buildingType.resourceGeneratorData;
    generatorCooldown = resourceGeneratorData.generatorCooldown;
  }

  private void Start()
  {
    Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);
    int nearbyResourceNodes = 0;
    foreach (Collider2D collider2D in collider2DArray)
    {
      ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
      if (resourceNode != null && resourceNode.resourceType == resourceGeneratorData.resourceType)
      {
        nearbyResourceNodes++;
      }
    }

    nearbyResourceNodes = Mathf.Clamp(nearbyResourceNodes, 0, resourceGeneratorData.maxResourceCollection);

    if (nearbyResourceNodes == 0)
    {
      enabled = false; // disable to avoid wasted calls
    }
    else
    {
      float a = resourceGeneratorData.generatorCooldown * 2;
      float b = resourceGeneratorData.generatorCooldown / 2;
      float t = nearbyResourceNodes / resourceGeneratorData.maxResourceCollection;
      generatorCooldown = Mathf.Lerp(a, b, t);
    }
  }

  void Update()
  {
    timeSinceGeneration += Time.deltaTime;
    if (timeSinceGeneration >= resourceGeneratorData.generatorCooldown)
    {
      timeSinceGeneration = 0f;
      ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
    }
  }
}
