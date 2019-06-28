using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public enum StatType { Attack, Defence, Other }

    [SerializeField] private string statName;
    [SerializeField] private int baseValue;
    [SerializeField] private StatType statType;
    [SerializeField] private Element element;

    private List<int> modifiers;
    
    public Stat()
    {
        modifiers = new List<int>();
    }

    public Stat(string statName, int baseValue)
    {
        this.statName = statName;
        this.baseValue = baseValue;
        modifiers = new List<int>();
    }

    // Get methods
    public int GetValue()
    {
        int finalValue = baseValue;
            modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public string GetStatName()
    {
        return statName;
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public Element GetElement()
    {
        return element;
    }

    // Set methods
    public void SetBaseValue(int newValue)
    {
        if (newValue >= 0)
            baseValue = newValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }

}

public enum Element { Physical, Fire, Water, Air, Other }
                    // Armor don't protect from "Other" element (such as status effects)
