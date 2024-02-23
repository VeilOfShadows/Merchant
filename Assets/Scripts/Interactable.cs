using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ItemObject item;
    public int amount;
    public Inventory playerInventory;
    public GameObject vfx;
    public GameObject mesh;
    public Collider collider;

    public void OnMouseDown()
    {
        amount = 1;
        NotificationManager.instance.DisplayNotification("+ " + item.data.itemName + " x " + amount);
        //NotificationManager.instance.DisplayNotification("- Item added to inventory: " + item.data.itemName + " x " + amount);

        collider.enabled = false;
        playerInventory.AddItem(item.data, amount);
        vfx.SetActive(false);
        mesh.SetActive(false);
    }
}
