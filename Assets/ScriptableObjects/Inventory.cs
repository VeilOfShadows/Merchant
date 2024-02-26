using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Create/Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public ItemDatabase database;
    public InventorySlot[] slots = new InventorySlot[28];
    public ItemObject coinItem;

    public void AddItem(Item _item, int _amount) {
        InventorySlot slot = FindItemInInventory(_item);

        if (!database.items[_item.itemID].data.stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return;
            //return true;
        }
        slot.AddAmount(_amount);
        //return true;
    }

    public bool AttemptPurchase(Item _item, int buyPrice)
    {
        if (!_item.tradeable)
        {
            return false;
        }

        InventorySlot temp = FindItemInInventory(coinItem.data);
        if (temp == null)
        {
            Debug.Log("No coin");
            return false;
        }
        else if (temp.amount < buyPrice)
        {
            Debug.Log("Not enough coin");
            return false;
        }

        temp.RemoveAmount(buyPrice);
        AddItem(_item, 1);
        return true;
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

    public InventorySlot FindFoodInInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemType == ItemType.Consumable)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item1.item.itemID == -1)
        {
            return;
        }

        if (item1.item.itemID != item2.item.itemID)
        {
            //if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            //{
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            //}
        }
        else
        {
            if (item2.item.stackable)
            {
                if (item2 != item1)
                {
                    item2.UpdateSlot(item2.item, item1.amount + item2.amount);
                    item1.RemoveItem();
                }
            }

        }
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

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot {
    public UserInterface parent; 
    public Item item = new Item();
    public int amount;
    public GameObject slotDisplay;

    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }

    public void UpdateSlot(Item _item, int _amount) {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        //if (amount <= 0)
        //{
        //      RemoveItem();
        //}
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }

    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    public void RemoveAmount(int value)
    {
        if ((amount - value) <= 0)
        {
            RemoveItem();
        }
        else
        {
            UpdateSlot(item, amount -= value);
        }
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }

}
