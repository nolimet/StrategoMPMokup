using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector2 mouseOffSet;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffSet = eventData.position - (Vector2)transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - mouseOffSet;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
