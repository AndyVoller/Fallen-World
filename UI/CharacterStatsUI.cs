using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsUI : IStatsUI
{
    public void UpdateUI(CharacterStats stats)
    {
        UpdateHealthBar(stats);
        UpdateMagickaBar(stats);
        UpdateStaminaBar(stats);
    }

    public void UpdateHealthBar(CharacterStats stats)
    {
        Image healthBar = stats.transform.Find("StatsUI/HealthBar/CurrentHealth").GetComponent<Image>();
        healthBar.fillAmount = 1f * stats.CurrentHealth / stats.GetMaxHealth();
    }

    public void UpdateMagickaBar(CharacterStats stats)
    {
        Image magickaBar = stats.transform.Find("StatsUI/MagickaBar/CurrentMagicka").GetComponent<Image>();
        magickaBar.fillAmount = 1f * stats.CurrentMagicka / stats.GetMaxMagicka();
    }

    public void UpdateStaminaBar(CharacterStats stats)
    {
        Image staminaBar = stats.transform.Find("StatsUI/StaminaBar/CurrentStamina").GetComponent<Image>();
        staminaBar.fillAmount = 1f * stats.CurrentStamina / stats.GetMaxStamina();
    }
}
