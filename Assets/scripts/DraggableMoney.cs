using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform parentAfterDrag;
    private Vector3 startPosition;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // bring to front
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If not dropped in DropArea, reset position
        if (transform.parent == transform.root)
        {
            rectTransform.anchoredPosition = startPosition;
            transform.SetParent(parentAfterDrag);
        }
    }

    // Reset when answer was wrong
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = startPosition;
        transform.SetParent(parentAfterDrag);
    }
}
