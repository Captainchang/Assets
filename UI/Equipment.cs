using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipment : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    [SerializeField]
    public Image EquipmentImage;
    [SerializeField]
    public Image EquipmentImage2;
    public void OnPointerClick(PointerEventData eventData)
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
                itemImage = slot.GetComponent<Image>();
                if (slot != null)
                {
                     UpdateUI();
                     Item removeitem = RemoveItem(slot);
                    if (removeitem != null)
                    {
                        //AddItemToEquipment(removeitem);
                    }
                }
            }
        }
    }
    public void UpdateUI()
    {
        if (itemImage != null & EquipmentImage != null)
        {
            EquipmentImage.sprite = itemImage.sprite;
            EquipmentImage.color = itemImage.color;
            EquipmentImage.enabled = itemImage.enabled;
        } 
    }
    public Item RemoveItem(Slot from)
    {
        Item removeItem = from.item;
        from.item = null;
        return removeItem;
    }
    public void AddItemToEquipment(Item item)
    {

    }
}
