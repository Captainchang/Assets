using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler , IEndDragHandler,IPointerClickHandler
{
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private Transform originParent;
    [SerializeField]
    private Vector3 originPosition;
    [SerializeField]
    private Inventory currentSlot;

    bool explanmenu;
    public GameObject menu;

    Vector3 returnpostion;
    private void Start()
    {

        returnpostion = new Vector3(0, 0,0);
        explanmenu = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        originParent = transform.parent;
        originPosition = transform.localPosition;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        menu.SetActive(true);
        menu.transform.position = transform.position + new Vector3(15, 10, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform); // 위치를 바꿀 준비를 위해  부모를 바꿈
        // To do  포지션을 마우스커서로.
        transform.localPosition = originPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        /*float dragSpeed = 15.0f;
        Vector2 justDelta = eventData.delta * dragSpeed * Time.deltaTime;
        rectTransform.anchoredPosition += eventData.delta;*/
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Slot"))
            {
                Slot slot = result.gameObject.GetComponent<Slot>();
                if (slot != null && gameObject.name != slot.name)
                {

                    SwapSlot(gameObject,slot);
              
                    menu.SetActive(true);
                    menu.transform.position = transform.position + new Vector3(15, 10, 0);

                    Debug.Log("아이템을 놓은 슬롯: " + slot.name);
                    Debug.Log("집은 슬롯 " + gameObject.name);
                    Debug.Log(slot.transform.localPosition);
                    Debug.Log(returnpostion);

                    if (gameObject.transform.localPosition != returnpostion)
                        slot.transform.localPosition = returnpostion;
                }
                slot = null;
            }
            else
            {
                Slotreturn();
            }
        }
    }
    private void Update()
    {
        if (transform.localPosition != returnpostion)
                transform.localPosition = returnpostion;
    }
    void Slotreturn()
    {
        transform.localPosition = originPosition;
        Debug.Log("슬롯이 아닌곳");
    }
    void SwapSlot(GameObject from, Slot to)
    {
        if (from.transform.parent == to.transform.parent)
            return;

        Transform parent1 = from.transform.parent;
        Transform parent2 = to.transform.parent;

        from.transform.SetParent(parent2,false);
        to.transform.SetParent(parent1,false);

        from.transform.localPosition = new Vector3(0, 0, 0 );
        to.transform.localPosition = new Vector3(0, 0, 0 );
    }
}
