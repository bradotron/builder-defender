using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private HealthSystem healthSystem;
  private Transform barTransform;

  private void Awake()
  {
    barTransform = transform.Find("bar");
  }

  private void Start()
  {
    healthSystem.OnDamaged += HealthSystem_OnDamaged;
    UpdateBar();
    UpdateHealthBarVisible();
  }

  private void HealthSystem_OnDamaged(object sender, EventArgs e)
  {
    UpdateBar();
    UpdateHealthBarVisible();
  }

  public void UpdateBar()
  {
    barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthNormalized(), 1, 1);
  }

  private void UpdateHealthBarVisible()
  {
    gameObject.SetActive(!healthSystem.IsFullHealth());
  }
}
