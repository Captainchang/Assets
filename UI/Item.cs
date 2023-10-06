using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class Item : ScriptableObject
{

    public string Itemtype;
    public string ItemName;
    public Sprite ItemIcon;
    public int ItemCount;
    public int ItemAttack;
    public int ItemHp;

    public Item(string itemtype, string itemName, Sprite itemIcon, int itemCount, int itemAttack,int itemHp)
    {
        Itemtype = itemtype;
        ItemName = itemName;
        ItemIcon = itemIcon;
        ItemCount = itemCount;
        ItemAttack= itemAttack;
        ItemHp = itemHp;
    }
}
