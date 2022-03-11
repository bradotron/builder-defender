using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
  private int currentHealthAmount;
  [SerializeField] private int maxHealthAmount;

  public event EventHandler OnDamaged;
  public event EventHandler OnHealed;
  public event EventHandler OnDied;

  private void Awake()
  {
    currentHealthAmount = maxHealthAmount;
  }

  public void SetMaxHealth(int newMaxHealth, bool updateCurrentHealth)
  {
    maxHealthAmount = newMaxHealth;

    if (updateCurrentHealth)
    {
      currentHealthAmount = maxHealthAmount;
    }
  }

  public int GetMaxHealth()
  {
    return maxHealthAmount;
  }

  public void Damage(int damageAmount)
  {
    currentHealthAmount -= damageAmount;
    currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, maxHealthAmount);
    OnDamaged?.Invoke(this, EventArgs.Empty);

    if (IsDead())
    {
      OnDied?.Invoke(this, EventArgs.Empty);
    }
  }

  public void Heal(int healAmount)
  {
    currentHealthAmount += healAmount;
    currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, maxHealthAmount);
    OnHealed?.Invoke(this, EventArgs.Empty);
  }

  public void HealFull()
  {
    currentHealthAmount = maxHealthAmount;
    OnHealed?.Invoke(this, EventArgs.Empty);
  }

  public bool IsDead()
  {
    return currentHealthAmount == 0;
  }

  public bool IsFullHealth()
  {
    return currentHealthAmount >= maxHealthAmount;
  }

  public int GetCurrentHealth()
  {
    return currentHealthAmount;
  }

  public float GetCurrentHealthNormalized()
  {
    return (float)currentHealthAmount / (float)maxHealthAmount;
  }
}
