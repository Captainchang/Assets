using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InventoryManager();
            }
            return instance;
        }
    }
    public Inventory inventory;
    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }
}
