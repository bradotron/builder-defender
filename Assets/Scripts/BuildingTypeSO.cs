using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
  public string sName;
  public Transform prefab;
  public ResourceGeneratorData resourceGeneratorData;
  public Sprite sprite;
  public Sprite constructionSprite;
  public float minConstructionRadius;
  public ResourceAmount[] constructionResourceCosts;
  public int maxHealthAmount;
  public float constructionTimerMax;

  public string GetConstructionResourceCostsString()
  {
    string constructionCostString = "";
    foreach (ResourceAmount resourceAmount in constructionResourceCosts)
    {
      constructionCostString += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.sName + ": " + resourceAmount.amount.ToString() + "</color>\n";
    }
    return constructionCostString;
  }
}
