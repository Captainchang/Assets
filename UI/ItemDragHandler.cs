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

    [SerializeField]
    private Inventory currentSlot;

    private void Start()
    {
        rectTransform= GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originParent = transform.parent;
        originPosition = transform.localPosition;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform); // ��ġ�� �ٲ� �غ� ����  �θ� �ٲ�
        // To do  �������� ���콺Ŀ����.
        transform.localPosition = originPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
          transform.SetParent(originParent);
          transform.localPosition = originPosition;
          Debug.Log("������ �ƴѰ�");
    }
}
