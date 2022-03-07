using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
  public static Camera mainCamera;
  public static Vector3 GetMouseWorldPosition()
  {
    mainCamera = mainCamera ?? Camera.main;

    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;
    return mouseWorldPosition;
  }

  public static Vector2 GetRandomDirection()
  {
    return new Vector2(
      UnityEngine.Random.Range(-1f, 1f),
      UnityEngine.Random.Range(-1f, 1f)
    ).normalized;
  }
}
