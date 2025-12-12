using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragDropHandler d = eventData.pointerDrag.GetComponent<DragDropHandler>();
        if (d != null)
        {
            d.transform.SetParent(transform); // Attach item to drop area
        }
    }
}
