using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
  public static EnemyWaveManager Instance { get; private set; }
  public event EventHandler OnWaveNumberChanged;
  private enum State
  {
    WaitingToSpawnWave,
    SpawningWave
  }

  private State state;
  [SerializeField]
  private List<Transform> spawnPositionTransforms;
  [SerializeField]
  private Transform NextWaveSpawnPosition;
  public int waveNumber { get; private set; }
  private float nextWaveSpawnTimer;
  private float nextEnemySpawnTimer;
  private int remainingEnemySpawnCount;
  private Vector3 spawnPosition;

  private void Awake()
  {
    Instance = this;
  }
  private void Start()
  {
    state = State.WaitingToSpawnWave;
    UpdateSpawnPosition();
    waveNumber = 0;
    nextWaveSpawnTimer = 10f;
  }

  private void Update()
  {
    switch (state)
    {
      case State.WaitingToSpawnWave:
        HandleWaitingToSpawnWave();
        break;

      case State.SpawningWave:
        HandleSpawningWave();
        break;

      default:
        break;
    }
  }

  private void HandleWaitingToSpawnWave()
  {
    nextWaveSpawnTimer -= Time.deltaTime;

    if (nextWaveSpawnTimer <= 0f)
    {
      SpawnWave();
      state = State.SpawningWave;
    }
  }

  private void HandleSpawningWave()
  {
    if (remainingEnemySpawnCount > 0)
    {
      nextEnemySpawnTimer -= Time.deltaTime;
      if (nextEnemySpawnTimer <= 0f)
      {
        SpawnEnemy();
        remainingEnemySpawnCount--;
        nextEnemySpawnTimer = UnityEngine.Random.Range(0.1f, 0.3f);
      }
    }
    else
    {
      nextWaveSpawnTimer = 10f;
      state = State.WaitingToSpawnWave;
      UpdateSpawnPosition();
    }
  }

  private void UpdateSpawnPosition()
  {
    spawnPosition = spawnPositionTransforms[UnityEngine.Random.Range(0, spawnPositionTransforms.Count)].position;
    NextWaveSpawnPosition.position = spawnPosition;
  }

  private void SpawnWave()
  {
    remainingEnemySpawnCount = 5 + (3 * waveNumber);
    waveNumber++;
    OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
  }

  private void SpawnEnemy()
  {
    Enemy.CreateAt(spawnPosition + (Vector3)Utils.GetRandomDirection() * UnityEngine.Random.Range(0f, 5f));
  }

  public int GetWaveNumber()
  {
    return waveNumber;
  }

  public float GetNextWaveSpawnTimer()
  {
    return nextWaveSpawnTimer;
  }

  public Vector3 GetSpawnPosition()
  {
    return spawnPosition;
  }
}
