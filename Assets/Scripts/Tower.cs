using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
  private Enemy targetEnemy;
  private float targetDetectionRadius = 20f;
  private float detectionCooldown = 0.2f;
  private float currentDetectionCooldown = 0f;
  [SerializeField] private float shootingCooldown = 0.3f;
  private float currentShootingCooldown = 0f;
  private Vector3 arrowProjectileSpawnPosition;
  private void Awake()
  {
    arrowProjectileSpawnPosition = transform.Find("arrowProjectileSpawnPosition").position;
  }
  private void Update()
  {
    HandleTargeting();
    HandleShooting();
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

  private void HandleShooting()
  {
    currentShootingCooldown += Time.deltaTime;
    currentShootingCooldown = Mathf.Clamp(currentShootingCooldown, 0f, shootingCooldown);

    if (targetEnemy != null && currentShootingCooldown >= shootingCooldown)
    {
      ArrowProjectile.CreateAt(arrowProjectileSpawnPosition, targetEnemy);
      currentShootingCooldown -= shootingCooldown;
    }
  }
  private void LookForCloserTarget()
  {
    Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetDetectionRadius);
    foreach (Collider2D collider2D in collider2Ds)
    {
      Enemy enemy = collider2D.transform.GetComponent<Enemy>();
      if (enemy != null)
      {
        if (targetEnemy == null)
        {
          targetEnemy = enemy;
        }
        else
        {
          if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(targetEnemy.transform.position, transform.position))
          {
            targetEnemy = enemy;
          }
        }
      }
    }
  }
}
