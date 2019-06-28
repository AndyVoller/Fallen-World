using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public int DamageAmount { get; private set; }
    public Element DamageElement { get; private set; }
        // Armor don't protect from "Other" element (such as bleeding effect)

    public Damage(int amount, Element element)
    {
        DamageAmount = amount;
        DamageElement = element;
    }

}
