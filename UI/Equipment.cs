using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipment : MonoBehaviour, IPointerClickHandler
{
    List<Item> addedItems = new List<Item>();

    [SerializeField]
    PlayerStat stat;
    public Image itemImage;
    [SerializeField]
    Image WeaponImag;
    [SerializeField]
    Image ArmorImg;
    [SerializeField]
    public Image EquipmentImage;
    [SerializeField]
    public Image EquipmentImage2;


    [SerializeField]
    public Image WeaponEquip;
    [SerializeField]
    public Image ArmorEquip;

    bool WeaponisEquip = false;
    bool ArmorisEquip = false;


    private void Start()
    {
        WeaponEquip.gameObject.SetActive(false);
        ArmorEquip.gameObject.SetActive(false);
    }

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

                if (!addedItems.Contains(slot.item))
                {
                    if (slot.item.Itemtype == "W" && !WeaponisEquip)
                    {

                        stat.Attack += slot.item.ItemAttack; 
                        stat.HP += slot.item.ItemHp;
                        stat.MaxHp += slot.item.ItemHp;

                        EqWeaponUpdateUI();
                        WeaponisEquip = true;

                        WeaponEquip.gameObject.SetActive(true);
                        WeaponEquip.transform.position = slot.transform.position + new Vector3(-20, 20,0);
                    }
                    else if (slot.item.Itemtype == "A" && !ArmorisEquip)
                    {

                        stat.Attack += slot.item.ItemAttack;
                        stat.HP += slot.item.ItemHp;
                        stat.MaxHp += slot.item.ItemHp;

                        EqArmorUpdateUI();
                        ArmorisEquip = true;

                        ArmorEquip.gameObject.SetActive(true);
                        ArmorEquip.transform.position = slot.transform.position + new Vector3(-20, 20, 0);
                    }
                    else { return; }

                    addedItems.Add(slot.item);
                }
                else if (addedItems.Contains(slot.item))
                {
                    if (slot.item.Itemtype == "W"&& WeaponisEquip)
                    {

                        stat.Attack -= slot.item.ItemAttack;
                        stat.HP -= slot.item.ItemHp;
                        stat.MaxHp -= slot.item.ItemHp;

                        UnEqWeaponUpdateUI();
                        WeaponisEquip = false;

                        WeaponEquip.gameObject.SetActive(false);

                    }
                    else if (slot.item.Itemtype == "A" && ArmorisEquip)
                    {
                        stat.Attack -= slot.item.ItemAttack;
                        stat.HP -= slot.item.ItemHp;
                        stat.MaxHp -= slot.item.ItemHp;

                        UnEqArmorUpdateUI();
                        ArmorisEquip = false;

                        ArmorEquip.gameObject.SetActive(false);
                    }
                    else { return; }
    
                    addedItems.Remove(slot.item);
                }
            }
        }
    }
    public void EqWeaponUpdateUI()
    {
        if (itemImage != null & EquipmentImage != null)
        {
            EquipmentImage.sprite = itemImage.sprite;
            EquipmentImage.color = itemImage.color;
            EquipmentImage.enabled = itemImage.enabled;
            itemImage = null;
        } 
    }
    public void UnEqWeaponUpdateUI()
    {
        Color newColor = ArmorImg.color;
        newColor.a = 0.2f;

        EquipmentImage.sprite = WeaponImag.sprite;
        EquipmentImage.color = newColor;
        EquipmentImage.enabled = WeaponImag.enabled;
    }
    public void EqArmorUpdateUI()
    {
        if (itemImage != null & EquipmentImage2 != null)
        {
            EquipmentImage2.sprite = itemImage.sprite;
            EquipmentImage2.color = itemImage.color;
            EquipmentImage2.enabled = itemImage.enabled;
            itemImage = null;
        }
    }
    public void UnEqArmorUpdateUI()
    {
        Color newColor = ArmorImg.color;
        newColor.a = 0.2f;

        EquipmentImage2.sprite = ArmorImg.sprite;
        EquipmentImage2.color = newColor;
        EquipmentImage2.enabled = ArmorImg.enabled;
           
    }
    public Item RemoveItem(Slot from)
    {
        Item removeItem = from.item;
        from.item = null;
        return removeItem;
    }
 
}
