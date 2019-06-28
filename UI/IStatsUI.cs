using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsUI
{
    void UpdateUI(CharacterStats stats);
    void UpdateHealthBar(CharacterStats stats);
    void UpdateMagickaBar(CharacterStats stats);
    void UpdateStaminaBar(CharacterStats stats);
}
