using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // A pocket represents the item slot in inventory
    // It contains identical items
    public List<Pocket> pockets { get; protected set; }
    public int MaxWeight { get; protected set; }
    public int TotalWeight
    {
        get
        {
            int weight = 0;
            pockets.ForEach(x => weight += x.Item.Weight * x.Count);
            return weight;
        }
    }

    void Awake()
    {
        pockets = new List<Pocket>();
        pockets.Add(new Pocket((Item)Resources.Load("Items/EquipItems/Gladius"), 1));
    }

    public void AddItems(Item item, int count)
    {
        Pocket pocket = pockets.Find(x => x.Item.Name == item.Name);

        if (pocket != null)
        {
            pocket.AddItems(count);
        }
        else
        {
            pockets.Add(new Pocket(item, count));
        }

        if(TotalWeight>MaxWeight)
        {
            GetComponent<CharacterController2D>().CanJump = false;
        }

    }

    public bool RemoveItems(Item item, int count)
    {
        Pocket pocket = pockets.Find(x => x.Item.Name == item.Name);
        if (pocket == null)
        {
            // Nothing to remove
            return false;
        }

        pocket.RemoveItems(1);
        if (pocket.Count <= 0)
            pockets.Remove(pocket);

        if (TotalWeight <= MaxWeight)
            GetComponent<CharacterController2D>().CanJump = true;

        return true;
    }

    public bool Contains(Item item)
    {
        Pocket pocket = pockets.Find(x => x.Item.Name == item.Name && x.Count > 0);
        if (pocket == null)
            return false;

        return true;
    }
}
