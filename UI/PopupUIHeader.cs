using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupUIHeader : MonoBehaviour,IBeginDragHandler,IDragHandler
{
    private RectTransform ParentRect;

    private Vector2 RectBegin;
    private Vector2 MoveBegin;
    private Vector2 MoveOffset;

    private void Awake()
    {
        ParentRect = transform.parent.GetComponent<RectTransform>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        RectBegin = ParentRect.anchoredPosition;
        MoveBegin = eventData.position;
    }   
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        MoveOffset = eventData.position - MoveBegin;
        ParentRect.anchoredPosition = RectBegin + MoveOffset;
    }
}
