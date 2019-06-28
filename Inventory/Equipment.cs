using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [SerializeField] protected EquipmentSlot equipSlot;
    public EquipmentSlot EquipSlot { get { return equipSlot; } }

    [SerializeField] protected List<Stat> stats = new List<Stat>();
    public List<Stat> Stats { get { return stats; } }

    protected bool isEquipped = false;
    public bool IsEquipped
    {
        get { return isEquipped; }
        set { isEquipped = value; }
    }

    public Stat GetStat(string statName)
    {
        foreach (Stat stat in stats)
        {
            if (stat.GetStatName() == statName)
            {
                return stat;
            }
        }
        return null;
    }

    public override void Use()
    {
        base.Use();
    }

}

public enum EquipmentSlot { Head, Chest, Hands, Legs, Feet, Weapon, Ring, Necklace }
