using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : IStatsUI
{
    public void UpdateUI(CharacterStats stats)
    {
        UpdateHealthBar(stats);
        UpdateMagickaBar(stats);
        UpdateStaminaBar(stats);
        UpdateExpInfo((PlayerStats)stats);
    }

    public void UpdateHealthBar(CharacterStats stats)
    {
        UIEventHandler.PlayerHealthChanged(stats.CurrentHealth, stats.GetMaxHealth());
    }

    public void UpdateMagickaBar(CharacterStats stats)
    {
        UIEventHandler.PlayerMagickaChanged(stats.CurrentMagicka, stats.GetMaxMagicka());
    }

    public void UpdateStaminaBar(CharacterStats stats)
    {
        UIEventHandler.PlayerStaminaChanged(stats.CurrentStamina, stats.GetMaxStamina());
    }

    public void UpdateExpInfo(PlayerStats stats)
    {
        UIEventHandler.PlayerXPChanged(stats.Experience, stats.XPToNextLevel, stats.Level);
    }
}
