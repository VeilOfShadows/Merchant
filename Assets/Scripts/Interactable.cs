using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public ItemObject item;
    public Inventory playerInventory;
    public GameObject vfx;
    public GameObject mesh;
    public Collider collider;

    public void OnMouseDown()
    {
        collider.enabled = false;
        playerInventory.AddItem(item.data, 1);
        vfx.SetActive(false);
        mesh.SetActive(false);
    }
}
