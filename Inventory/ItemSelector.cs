using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentManager equipmentManager;
    public static Item item { get; private set; }       // Currently selected item

    static InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public static void SelectItem(Item _item)
    {
        if (_item == item)
            return;

        if(item!=null)
        {
            UnselectItem();
        }

        item = _item;
        inventoryUI.UpdateInfo(item);
    }

    static void UnselectItem()
    {
        item = null;
        inventoryUI.HideInfo();
    }

    public void UseItem()
    {
        if (item is Equipment)
            return;

        if (inventory.RemoveItems(item, 1)) 
        {
            item.Use();
            inventoryUI.UpdateUI();
        }
    }

    public void EquipItem()
    {
        if (!(item is Equipment))
            return;

        equipmentManager.EquipItem(item as Equipment);
    }

    public void RemoveItem()
    {
        if(item is Equipment)
        {
            equipmentManager.UnequipItem(item as Equipment);
        }

        if (inventory.RemoveItems(item, 1))
        {
            inventoryUI.UpdateUI();
            if(!inventory.Contains(item))
            {
                item = null;
                inventoryUI.HideInfo();
            }
        }
    }

}
