using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyWaveUI : MonoBehaviour
{
  [SerializeField]
  private EnemyWaveManager enemyWaveManager;
  private Camera mainCamera;
  private TextMeshProUGUI waveNumberText;
  private TextMeshProUGUI waveMessageText;
  private RectTransform enemyWaveSpawnPositionIndicator;
  private RectTransform enemyClosestPositionIndicator;
  private void Awake()
  {
    waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
    waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
    enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
    enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
  }

  private void Start()
  {
    mainCamera = Camera.main;
    enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
    SetWaveNumberText("Wave " + this.enemyWaveManager.GetWaveNumber());
  }

  private void Update()
  {
    HandleNextWaveMessage();
    HandleWaveSpawnIndicator();
    HandleClosestEnemyIndicator();
  }

  private void HandleNextWaveMessage()
  {
    float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
    if (nextWaveSpawnTimer <= 0f)
    {
      SetMessageText("");
    }
    else
    {
      SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
    }
  }

  private void HandleWaveSpawnIndicator()
  {
    Vector3 dirToNextSpawn = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
    enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawn * 300f;
    enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleOfVector(dirToNextSpawn));
    float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
    enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
  }

  private void HandleClosestEnemyIndicator()
  {
    Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(mainCamera.transform.position, 9999f);
    Enemy closestEnemy = null;
    foreach (Collider2D collider2D in collider2Ds)
    {
      Enemy enemy = collider2D.transform.GetComponent<Enemy>();
      if (enemy != null)
      {
        if (closestEnemy == null)
        {
          closestEnemy = enemy;
        }
        else
        {
          if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
          {
            closestEnemy = enemy;
          }
        }
      }
    }

    if (closestEnemy != null)
    {
      Vector3 dirToClosestEnemy = (closestEnemy.transform.position - mainCamera.transform.position).normalized;
      enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
      enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, Utils.GetAngleOfVector(dirToClosestEnemy));
      float distanceToClosestEnemy = Vector3.Distance(closestEnemy.transform.position, mainCamera.transform.position);
      enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
    }
    else
    {
      enemyClosestPositionIndicator.gameObject.SetActive(false);
    }
  }

  private void SetMessageText(string message)
  {
    waveMessageText.SetText(message);
  }

  private void SetWaveNumberText(string message)
  {
    waveNumberText.SetText(message);
  }

  private void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
  {
    SetWaveNumberText("Wave " + this.enemyWaveManager.GetWaveNumber());
  }
}
