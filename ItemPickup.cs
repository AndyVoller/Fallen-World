using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item item;
    [SerializeField] private int count;

    public override void Interact()
    {
        base.Interact();
        PickupItem();
    }

    void PickupItem()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.AddItems(item, count);
        interactText.SetActive(false);
        Destroy(this);                      // Game object is not interactable now
    }

}
