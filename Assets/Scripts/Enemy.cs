using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public static Enemy CreateAt(Vector3 position)
  {
    Transform pfEnemy = Resources.Load<Transform>("pf_Enemy");
    Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

    Enemy enemy = enemyTransform.GetComponent<Enemy>();
    return enemy;
  }
  private Transform targetBuildingTransform;
  private Rigidbody2D rb2D;
  private HealthSystem healthSystem;
  private float targetDetectionRadius = 10f;
  private float detectionCooldown = 0.3f;
  private float currentDetectionCooldown = 0f;

  private void Awake()
  {
    rb2D = GetComponent<Rigidbody2D>();
    currentDetectionCooldown = UnityEngine.Random.Range(0f, detectionCooldown);
  }
  private void Start()
  {
    healthSystem = GetComponent<HealthSystem>();
    healthSystem.OnDied += HealthSystem_OnDied;
    TargetHQ();
  }

  private void Update()
  {
    HandleMovement();
    HandleTargeting();
  }

  private void HandleMovement()
  {
    if (targetBuildingTransform != null)
    {
      Vector2 moveDirNorm = (targetBuildingTransform.transform.position - transform.position).normalized;
      float moveSpeed = 6f;
      rb2D.velocity = moveDirNorm * moveSpeed;
    }
    else
    {
      rb2D.velocity = Vector2.zero;
    }

  }

  private void HandleTargeting()
  {
    currentDetectionCooldown += Time.deltaTime;
    if (currentDetectionCooldown >= detectionCooldown)
    {
      currentDetectionCooldown -= detectionCooldown;
      LookForCloserTarget();
    }
  }
  private void OnCollisionEnter2D(Collision2D collision2D)
  {
    Building building = collision2D.gameObject.GetComponent<Building>();

    if (building != null)
    {
      HealthSystem buildingHealthSystem = building.GetComponent<HealthSystem>();
      buildingHealthSystem.Damage(10);
      Destroy(gameObject);
    }
  }

  private void LookForCloserTarget()
  {
    Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetDetectionRadius);
    foreach (Collider2D collider2D in collider2Ds)
    {
      Building building = collider2D.transform.GetComponent<Building>();
      if (building != null)
      {
        if (targetBuildingTransform == null)
        {
          targetBuildingTransform = building.transform;
        }
        else
        {
          if (Vector3.Distance(building.transform.position, transform.position) < Vector3.Distance(targetBuildingTransform.position, transform.position))
          {
            targetBuildingTransform = building.transform;
          }
        }
      }
    }
    if (targetBuildingTransform == null)
    {
      TargetHQ();
    }
  }

  private void TargetHQ()
  {
    if (BuildingManager.Instance.GetHQBuilding() != null)
    {
      targetBuildingTransform = BuildingManager.Instance.GetHQBuilding().transform;
    }
  }

  private void HealthSystem_OnDied(object sender, EventArgs e)
  {
    Destroy(gameObject);
  }
}
