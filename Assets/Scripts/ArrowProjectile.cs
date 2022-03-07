using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
  public static ArrowProjectile CreateAt(Vector3 position, Enemy enemy)
  {
    Transform pfArrowProjectile = Resources.Load<Transform>("pf_ArrowProjectile");
    Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

    ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();

    arrowProjectile.SetTarget(enemy);
    return arrowProjectile;
  }

  private Enemy targetEnemy;
  private float moveSpeed = 20f;
  private float timeToDie = 2f;
  private Vector3 lastMoveDirection;
  private void SetTarget(Enemy enemy)
  {
    targetEnemy = enemy;
  }
  private void Update()
  {
    Vector3 moveDirection;
    if (targetEnemy != null)
    {
      moveDirection = targetEnemy.transform.position - transform.position;
      lastMoveDirection = moveDirection;
    }
    else
    {
      moveDirection = lastMoveDirection;
    }
    transform.position += moveDirection * moveSpeed * Time.deltaTime;
    transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleOfVector(moveDirection));

    timeToDie -= Time.deltaTime;
    if (timeToDie <= 0f)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D collider2D)
  {
    Enemy enemy = collider2D.gameObject.GetComponent<Enemy>();
    if (enemy != null)
    {
      int damageAmount = 10;
      enemy.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
      Destroy(gameObject);
    }
  }
}
