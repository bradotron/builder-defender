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

}
