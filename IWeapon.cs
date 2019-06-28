using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    List<Stat> Stats { get; set; }
    Damage Damage { get; set; }
    int UpgradeLevel { get; set; }              
    bool Upgrade();
}
