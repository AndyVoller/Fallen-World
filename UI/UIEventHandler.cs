using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void MainStatEventHandler(int currentValue, int maxValue);
    public static event MainStatEventHandler OnPlayerHealthChanged;
    public static event MainStatEventHandler OnPlayerMagickaChanged;
    public static event MainStatEventHandler OnPlayerStaminaChanged;

    public delegate void XPEventHandler(int currentXP, int xpToLevelUp, int level);
    public static event XPEventHandler OnPlayerXPChanged;

    public delegate void SkillsEventHandler(CharacterSkills skills);
    public static event SkillsEventHandler OnPlayerSkillsChanged;

    public static void PlayerHealthChanged(int currentValue, int maxValue)
    {
        OnPlayerHealthChanged(currentValue, maxValue);
    }

    public static void PlayerMagickaChanged(int currentValue, int maxValue)
    {
        OnPlayerMagickaChanged(currentValue, maxValue);
    }

    public static void PlayerStaminaChanged(int currentValue, int maxValue)
    {
        OnPlayerStaminaChanged(currentValue, maxValue);
    }

    public static void PlayerXPChanged(int currentXP, int xpToLevelUp, int level)
    {
        OnPlayerXPChanged(currentXP, xpToLevelUp, level);
    }

    public static void PlayerSkillsChanged(CharacterSkills skills)
    {
        OnPlayerSkillsChanged(skills);
    }
}
