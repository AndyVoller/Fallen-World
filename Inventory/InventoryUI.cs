using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;       
    [Header("Prefabs")]
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject itemStatsPrefab;
    [Header("UI elements")]
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Transform descriptionPanel;
    [SerializeField] private Transform itemStatsPanel;
    [SerializeField] private Transform buttonsPanel;

    List<InventorySlot> slots = new List<InventorySlot>();
    List<Transform> itemStats = new List<Transform>();

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateItemSlots();
    }

    void UpdateItemSlots()
    {
        // Remove slots
        foreach(InventorySlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        // Create slots
        foreach(Pocket pocket in inventory.pockets)
        {
            InventorySlot slot = Instantiate(inventorySlotPrefab, itemsParent).GetComponent<InventorySlot>();
            slot.AddItem(pocket.Item, pocket.Count);
            slots.Add(slot);
        }
    }

    public void UpdateInfo(Item item)
    {
        Image itemIcon = descriptionPanel.Find("ItemIcon").GetComponent<Image>();
        Text itemName = descriptionPanel.Find("ItemName").GetComponent<Text>();
        Text itemDescription = descriptionPanel.Find("ItemDescription").GetComponent<Text>();

        itemIcon.sprite = item.Icon;
        itemName.text = item.Name;
        itemDescription.text = item.Description;

        // Update item stats info
        ClearItemStats();
        if(item is Equipment)
        {
            foreach(Stat stat in (item as Equipment).Stats)
            {
                Transform itemStat = Instantiate(itemStatsPrefab, itemStatsPanel).GetComponent<Transform>();
                itemStat.GetChild(0).GetComponent<Text>().text = stat.GetStatName();
                itemStat.GetChild(1).GetComponent<Text>().text = stat.GetValue().ToString();
                itemStats.Add(itemStat);
            }
        }
    }

    void ClearItemStats()
    {
        foreach (Transform itemStat in itemStats)
        {
            Destroy(itemStat.gameObject);
        }
        itemStats.Clear();
    }

    public void HideInfo()
    {
        Image itemIcon = descriptionPanel.Find("ItemIcon").GetComponent<Image>();
        Text itemName = descriptionPanel.Find("ItemName").GetComponent<Text>();
        Text itemDescription = descriptionPanel.Find("ItemDescription").GetComponent<Text>();

        itemIcon.sprite = null;
        itemName.text = "";
        itemDescription.text = "";

        ClearItemStats();
    }

}
