using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public List<Stat> Stats { get; set; }
    public Damage Damage { get; set; }

    public int UpgradeLevel { get; set; }
    public static int MaxUpgradeLevel { get { return 5; } }

    void Start()
    {
        UpgradeLevel = 0;
    }

    public bool Upgrade()
    {
        if (UpgradeLevel >= MaxUpgradeLevel)
            return false;

        UpgradeLevel++;
        return true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Damage == null)
            return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<CharacterStats>().TakeDamage(Damage);
        }
    }
}
