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

    public int slotIndex; // ���� �ε���

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
       // transform.SetParent(transform.parent.parent); // ��ġ�� �ٲ� �غ� ����  �θ� �ٲ�
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
        

        //  To do  �̰Ÿ��� ���� ������ ������.
        if (hit.collider != null && hit.collider.CompareTag("Slot"))
        {
            transform.SetParent(hit.collider.transform);
            transform.localPosition = Vector3.zero;
            slotIndex = hit.collider.GetComponent<ItemDragHandler>().slotIndex;
        }
        else
        {
            //���� ��ġ��
            transform.SetParent(originParent);
            transform.localPosition = originPosition;
        }
    }
}
