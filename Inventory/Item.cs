using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected new string name = "New item";
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon = null;
    [SerializeField] protected int weight = 0;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public Sprite Icon { get { return icon; } }
    public int Weight { get { return weight; } }

    public virtual void Use()
    {
        
    }

}
