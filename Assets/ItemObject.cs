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
public class ItemObject : ScriptableObject
{
    //public string itemName;
    //public ItemType itemType;
    //public int itemID = -1;
    //[TextArea(10,25)]
    //public string itemDescription;
    public bool stackable;
    public Item data = new Item();
}

[System.Serializable]
public class Item {
    public string itemName;
    public ItemType itemType;
    public int itemID = -1;
    public int baseCoinValue = -1;

    public Item() 
    {
        itemName = "";
        itemID = -1;
    }
    public Item(ItemObject item)
    {
        itemName = item.name;
        itemType = item.data.itemType;
        itemID = item.data.itemID;
        baseCoinValue = item.data.baseCoinValue;
    }
}
