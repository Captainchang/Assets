using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject SlotPrefab;
    public Transform SlotParent;
    public Item[] Items;
    void Start()
    {
       Items = new Item[104];

        for (int i = 0; i < Items.Length; i++)
        {
            
            GameObject slot = Instantiate(SlotPrefab, SlotParent);
            slot.name = "Slot" + (i + 1);
            slot.SetActive(true);

            if (Items[i] != null)
            {
                Image IconImage = slot.GetComponent<Image>();
                IconImage.sprite = Items[i].ItemIcon;
                Text itemCountText = slot.transform.GetChild(0).GetComponent<Text>();
                itemCountText.text = Items[i].ItemCount.ToString();

            }
        }
    }
    public void AddItem(Item newItem)
    {
        for (int i =0; i< Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = newItem;

                GameObject slot = SlotParent.GetChild(i).gameObject;
                Image IconImage = slot.GetComponent<Image>();
                IconImage.sprite = newItem.ItemIcon;
                //Text itemCountText = slot.transform.GetChild(0).GetComponent<Text>();
                // itemCountText.text = Items[i].ItemCount.ToString();
                break;
            }
        }
    }
}
