using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
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
        FreshSlot();
    }
   
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
            print("������ ���� �� �ֽ��ϴ�");
        }
    }
}
