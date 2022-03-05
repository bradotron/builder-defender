using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
  public string sName;
  public string sNameShort;
  public Sprite sprite;
  public string colorHex;
}
