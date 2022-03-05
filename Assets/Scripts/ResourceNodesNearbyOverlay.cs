using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceNodesNearbyOverlay : MonoBehaviour
{
  private TextMeshPro textComponent;
  private SpriteRenderer iconSpriteRenderer;
  private ResourceGeneratorData resourceGeneratorData;
  private void Awake()
  {
    textComponent = transform.Find("text").GetComponent<TextMeshPro>();
    iconSpriteRenderer = transform.Find("icon").GetComponent<SpriteRenderer>();

    Hide();
  }

  private void Update()
  {
    if (gameObject.activeInHierarchy)
    {
      float nearbyResourceNodes = ResourceGenerator.GetNearbyResourceNodeCount(transform.position, resourceGeneratorData);
      float percentEfficiency = Mathf.Clamp(nearbyResourceNodes / resourceGeneratorData.maxResourceCollection, 0f, 1f);
      textComponent.SetText(((int)(percentEfficiency * 100f)).ToString() + "%");
    }
  }
  public void Show(ResourceGeneratorData resourceGeneratorData)
  {
    this.resourceGeneratorData = resourceGeneratorData;
    iconSpriteRenderer.sprite = resourceGeneratorData.resourceType.sprite;
    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }

}
