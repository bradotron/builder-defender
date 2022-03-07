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

// returns the angle of the vector in degrees; 0 degrees is along the positive x-axis to the right
// 
  public static float GetAngleOfVector(Vector3 vector) {
    float radians = Mathf.Atan2(vector.y, vector.x);
    return radians * Mathf.Rad2Deg;
  }
}
