using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EquipmentManager : MonoBehaviour
{
    protected List<Equipment> equipment = new List<Equipment>();
    [SerializeField] protected Transform rightHand;
    protected CharacterStats characterStats;

    public GameObject Weapon { get; private set; }

    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        EquipItem((Equipment)Resources.Load("Items/EquipItems/Gladius"));
    }

    public void EquipItem(Equipment itemToEquip)
    {
        Equipment prevItem = equipment.Find(i => i.EquipSlot == itemToEquip.EquipSlot);
        if (prevItem != null)
        {
            UnequipItem(prevItem);
        }

        equipment.Add(itemToEquip);
        foreach (Stat stat in itemToEquip.Stats)
        {
            characterStats.GetStat(stat.GetStatName()).AddModifier(stat.GetValue());
        }
        
        if(itemToEquip.EquipSlot==EquipmentSlot.Weapon)
        {
            Weapon = (GameObject)Instantiate(Resources.Load(itemToEquip.Name), rightHand);
        }
    }

    public void UnequipItem(Equipment itemToUnequip)
    {
        if(!equipment.Contains(itemToUnequip))
        {
            return;
        }

        foreach(Stat stat in itemToUnequip.Stats)
        {
            characterStats.GetStat(stat.GetStatName()).RemoveModifier(stat.GetValue());
        }

        equipment.Remove(itemToUnequip);

        if (itemToUnequip.EquipSlot == EquipmentSlot.Weapon)
        {
            Destroy(Weapon);
            Weapon = null;
        }
    }
}
