using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int Level { get; private set; }
    public int XPToNextLevel { get { return 1000 + (Level - 1) * 500; } }
    public int Perks { get; set; }
    public int Experience { get; private set; }

    void Start()
    {
        GetStartStatsValues();

        statsUI = new PlayerStatsUI();
        statsUI.UpdateUI(this);
        StartCoroutine("RegenStats");
    }

    protected override void GetStartStatsValues()
    {
        CurrentHealth = maxHealth;
        CurrentMagicka = maxMagicka;
        CurrentStamina = maxStamina;

        HealthRegen = 3;
        MagickaRegen = 10;
        StaminaRegen = 5;

        Level = 1;
        Experience = 0;
        Perks = 0;
    }

    public void AddExperience(int expToAdd)
    {
        if (Level > 99)             // Max 100 lvl
            return;

        Experience += expToAdd;
        // Check for Level Up
        while (Experience >= XPToNextLevel)
        {
            Experience -= XPToNextLevel;
            ++Level;
            ++Perks;
        }

        (statsUI as PlayerStatsUI).UpdateExpInfo(this);
    }

    protected override void Die()
    {
        Debug.Log(transform.name + " died.");
        CurrentHealth = maxHealth;
    }
}
