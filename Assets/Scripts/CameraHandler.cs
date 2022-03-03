using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
  [SerializeField] private CinemachineVirtualCamera virtualCamera;

  private float moveSpeed = 30f;

  private float orthographicSize;
  private float targetOrthographicSize;
  private float zoomMulti = 2f;
  private float minOrthographicSize = 10f;
  private float maxOrthographicSize = 30f;
  private float zoomSpeedMulti = 5f;

  // Start is called before the first frame update
  void Start()
  {
    orthographicSize = virtualCamera.m_Lens.OrthographicSize;
    targetOrthographicSize = orthographicSize;
  }

  // Update is called once per frame
  void Update()
  {
    HandleMovement();
    HandleZoom();
  }

  private void HandleMovement()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    Vector3 moveDir = new Vector3(x, y).normalized;

    transform.position += moveDir * moveSpeed * Time.deltaTime;
  }

  private void HandleZoom()
  {
    targetOrthographicSize += -Input.mouseScrollDelta.y * zoomMulti;
    targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

    orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeedMulti);

    virtualCamera.m_Lens.OrthographicSize = orthographicSize;
  }
}
