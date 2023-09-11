using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler , IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originParent;
    [SerializeField]
    private Vector3 originPosition;

    public int slotIndex; // 슬롯 인덱스

    private void Start()
    {
        rectTransform= GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originParent = transform.parent;
        originPosition = transform.localPosition;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       // canvasGroup.blocksRaycasts = false;
       // transform.SetParent(transform.parent.parent); // 위치를 바꿀 준비를 위해  부모를 바꿈
        transform.localPosition = originPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        

        //  To do  이거말고 슬롯 스왑을 만들자.
        if (hit.collider != null && hit.collider.CompareTag("Slot"))
        {
            transform.SetParent(hit.collider.transform);
            transform.localPosition = Vector3.zero;
            slotIndex = hit.collider.GetComponent<ItemDragHandler>().slotIndex;
        }
        else
        {
            //원래 위치로
            transform.SetParent(originParent);
            transform.localPosition = originPosition;
        }
    }
}
