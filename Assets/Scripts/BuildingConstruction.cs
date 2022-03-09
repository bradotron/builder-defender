using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
  public static BuildingConstruction CreateAt(Vector3 position, BuildingTypeSO buildingTypeSO)
  {
    Transform pfBuildingConstruction = Resources.Load<Transform>("pf_BuildingConstruction");
    Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

    BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
    buildingConstruction.SetBuildingType(buildingTypeSO);

    return buildingConstruction;
  }

  private float constructionTimer;
  private float constructionTimerMax;
  private BuildingTypeSO buildingTypeSO;
  private BuildingTypeReference buildingTypeReference;
  private BoxCollider2D boxCollider2D;
  private SpriteRenderer spriteRenderer;
  private Material constructionMaterial;
  private void Awake()
  {
    boxCollider2D = GetComponent<BoxCollider2D>();
    spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
    buildingTypeReference = GetComponent<BuildingTypeReference>();
    constructionMaterial = spriteRenderer.material;
  }

  private void Start() {
  }

  private void Update()
  {
    constructionTimer -= Time.deltaTime;
    constructionMaterial.SetFloat("_Progress", 1 - GetConstructionTimerNormalized());
    if (constructionTimer <= 0)
    {
      Instantiate(buildingTypeSO.prefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
  }

  private void SetBuildingType(BuildingTypeSO buildingTypeSO)
  {
    this.buildingTypeSO = buildingTypeSO;
    this.constructionTimer = this.buildingTypeSO.constructionTimerMax;

    boxCollider2D.offset = this.buildingTypeSO.prefab.GetComponent<BoxCollider2D>().offset;
    boxCollider2D.size = this.buildingTypeSO.prefab.GetComponent<BoxCollider2D>().size;

    buildingTypeReference.buildingType = buildingTypeSO;

    spriteRenderer.sprite = buildingTypeSO.sprite;
  }

  public float GetConstructionTimerNormalized()
  {
    return constructionTimer / buildingTypeSO.constructionTimerMax;
  }
}
