using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipUI : MonoBehaviour
{
  public static TooltipUI Instance { get; private set; }
  [SerializeField] private RectTransform canvasRectTransform;
  private RectTransform rectTransform;
  private TextMeshProUGUI textMeshProUGUI;
  private RectTransform backgroundRectTransform;
  private bool isRectSizeUpdated = false;
  private TooltipTimer tooltipTimer;
  // Start is called before the first frame update
  void Awake()
  {
    Instance = this;

    rectTransform = GetComponent<RectTransform>();
    textMeshProUGUI = transform.Find("text").GetComponent<TextMeshProUGUI>();
    backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

    SetText("Test Text Please Ignore!");
    Hide();
  }

  private void Update()
  {
    UpdateTooltipPosition();

    if (!isRectSizeUpdated)
    {
      UpdateBackgroundRectSize();
      isRectSizeUpdated = true;
    }

    if (tooltipTimer != null)
    {
      tooltipTimer.timer -= Time.deltaTime;
      if (tooltipTimer.timer <= 0f)
      {
        Hide();
      }
    }
  }

  private void UpdateTooltipPosition()
  {
    Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

    if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
    {
      anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
    }
    if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
    {
      anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
    }

    rectTransform.anchoredPosition = anchoredPosition;
  }

  private void SetText(string text)
  {
    UpdateTooltipPosition();
    textMeshProUGUI.SetText(text);
    isRectSizeUpdated = false;
  }

  private void UpdateBackgroundRectSize()
  {
    textMeshProUGUI.ForceMeshUpdate();
    Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
    Vector2 padding = new Vector2(12, 8);
    backgroundRectTransform.sizeDelta = textSize + padding;
  }

  public void Show(string text, TooltipTimer tooltipTimer = null)
  {
    SetText(text);
    gameObject.SetActive(true);
    this.tooltipTimer = tooltipTimer;
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }

  public class TooltipTimer
  {
    public float timer;
  }
}
