using UnityEngine;

public class FollowWorldObjectUI : MonoBehaviour
{
    [SerializeField] private Transform targetWorldObject;
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 0.8f, 0);
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
    }

    private void LateUpdate()
    {
        if (targetWorldObject == null || canvas == null) return;

        Vector3 worldPos = targetWorldObject.position + worldOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out anchoredPos);

        rectTransform.anchoredPosition = anchoredPos;
    }
}
