using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    public Slot[] slots;
    [SerializeField]
    GameObject SlotPrefab;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        //slots = new Slot[35];
    }
    private void Awake()
    {
   
        //SlotCreate();
        FreshSlot();
    }
    /*public void SlotCreate()
    {
        for (int j = 0; j > slots.Length; j++)
        {
            GameObject slot = Instantiate(SlotPrefab, slotParent);
            slot.name = "Slot" + (j + 1);
            slot.SetActive(true);
        }
    }*/
    public void FreshSlot()
    {
        int i = 0;
        for (; i< items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i< slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù");
        }
    }
}
