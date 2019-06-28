using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUIPanel : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    // Main panels
    Transform mainStats;
    Transform otherStats;

    // Main stats
    Transform levelPanel;
    Transform maxHealth;
    Transform maxMagicka;
    Transform maxStamina;


    // Other stats
    Transform attackPanel;
    Transform defencePanel;

    void Awake()
    {
        mainStats = transform.Find("MainStats");
        otherStats = transform.Find("OtherStats");

        levelPanel = mainStats.Find("LevelPanel");
        maxHealth = mainStats.Find("MaxHealth");
        maxMagicka = mainStats.Find("MaxMagicka");
        maxStamina = mainStats.Find("MaxStamina");

        attackPanel = otherStats.Find("AttackPanel");
        defencePanel = otherStats.Find("DefencePanel");

        UpdateUI();
    }

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateLevelPanel();
        UpdateMainStats();
        UpdateOtherStats();
    }

    void UpdateLevelPanel()
    {
        Transform fillImage = levelPanel.GetChild(0);
        Image lvlImage = fillImage.GetComponent<Image>();
        Text lvlValueText = fillImage.GetChild(0).Find("LvlValueText").GetComponent<Text>();

        lvlImage.fillAmount = 1f * playerStats.Experience / playerStats.XPToNextLevel;
        lvlValueText.text = playerStats.Level.ToString();
    }

    void UpdateMainStats()
    {
        Text maxHealthText = maxHealth.Find("StatValue").GetComponent<Text>();
        Text maxMagickaText = maxMagicka.Find("StatValue").GetComponent<Text>();
        Text maxStaminaText = maxStamina.Find("StatValue").GetComponent<Text>();

        maxHealthText.text = playerStats.GetMaxHealth().ToString();
        maxMagickaText.text = playerStats.GetMaxMagicka().ToString();
        maxStaminaText.text = playerStats.GetMaxStamina().ToString();
    }

    void UpdateOtherStats()
    {
        // Next free slots positions
        int attackChildIndex = 0;
        int defenceChildIndex = 0;

        Transform statChild;

        foreach (Stat stat in playerStats.GetStats())
        {
            if(stat.GetStatType()==Stat.StatType.Attack)            // Fill attack panel slot
            {
                statChild = attackPanel.GetChild(attackChildIndex);
                FillStatSlot(statChild, stat);
                attackChildIndex++;
            }
            else if (stat.GetStatType() == Stat.StatType.Defence)   // Fill defence panel slot
            {
                statChild = defencePanel.GetChild(defenceChildIndex);
                FillStatSlot(statChild, stat);
                defenceChildIndex++;
            }
        }
    }

    void FillStatSlot(Transform slot, Stat stat)
    {
        if (slot == null)
            return;

        Text statName = slot.Find("StatName").GetComponent<Text>();
        Text statValue = slot.Find("StatValue").GetComponent<Text>();
        statName.text = stat.GetStatName();
        statValue.text = stat.GetValue().ToString();
    }

}
