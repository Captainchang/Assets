using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class Item : ScriptableObject
{

    public string ItemName;
    public Sprite ItemIcon;
    public int ItemCount;

    public Item(string itemName, Sprite itemIcon, int itemCount)
    {
        ItemName = itemName;
        ItemIcon = itemIcon;
        ItemCount = itemCount;
    }
}
