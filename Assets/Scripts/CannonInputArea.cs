using UnityEngine;
using UnityEngine.EventSystems;

public class CannonInputArea : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    public Cannon cannon;

    public void OnPointerDown(PointerEventData eventData)
    {
        cannon.BeginAim(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cannon.UpdateAim(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cannon.EndAim(eventData.position);
    }
}