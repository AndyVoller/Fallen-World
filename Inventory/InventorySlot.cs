using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class InventorySlot : MonoBehaviour
{
    private Item item;
    private int count;

    Image icon;
    Text countText;

    void Awake()
    {
        countText = transform.GetChild(0).GetComponent<Text>();
        icon = GetComponent<Image>();
    }

    public void AddItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(item==null)
        {
            ClearSlot();
            return;
        }

        icon.sprite = item.Icon;
        countText.text = count.ToString();
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        Destroy(this.gameObject);
    }

    public void OnButtonClick()
    {
        ItemSelector.SelectItem(item);
    }

}
