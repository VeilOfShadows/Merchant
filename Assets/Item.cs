using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public enum ItemType { 
    DEFAULT,
    Consumable,
    Commodity,
    Equipment
}

[CreateAssetMenu(fileName = "Item", menuName = "Create/Item/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int itemID = -1;
    [TextArea(10,25)]
    public string itemDescription;
}
