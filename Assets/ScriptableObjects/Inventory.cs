using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Create/Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public ItemDatabase database;
    public InventorySlot[] slots = new InventorySlot[28];

    public void AddItem(Item _item, int _amount) {
        InventorySlot slot = FindItemInInventory(_item);

        if (!database.items[_item.itemID].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return;
            //return true;
        }
        slot.AddAmount(_amount);
        //return true;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemID <= -1)
            {
                slots[i].UpdateSlot(_item, _amount);
                return slots[i];
            }
        }
        return null;
    }

    public InventorySlot FindItemInInventory(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemID == _item.itemID)
            {
                return slots[i];
            }
        }
        return null;
    }

    [ContextMenu("CLEAR")]
    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
        }
    }

    [ContextMenu("Sync")]
    public void Sync()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = database.FindItem(slots[i].item.itemID);
        }
    }
}

[System.Serializable]
public class InventorySlot {
    public UserInterface parent; 
    public Item item = new Item();
    public int amount;
    public GameObject slotDisplay;
public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }

    public void UpdateSlot(Item _item, int _amount) {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }

}
