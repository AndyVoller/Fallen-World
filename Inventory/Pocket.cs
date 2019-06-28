using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket
{
    public Item Item { get; set; }
    public int Count { get; set; }

    public Pocket(Item item, int count)
    {
        Item = item;
        Count = count;
    }

    public void AddItems(int countToAdd)
    {
        Count += countToAdd;
    }

    public void RemoveItems(int countToRemove)
    {
        if (Count < countToRemove)
        {
            Debug.LogWarning("Not enough items in inventory");
            return;
        }
        Count -= countToRemove;
    }

}
