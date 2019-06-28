using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image magickaBar;
    [SerializeField] private Image staminaBar;

    [Header("Level Panel")]
    [SerializeField] private Image expImage;
    [SerializeField] private Text lvlText;

    [Header("Skills")]
    [SerializeField] private GameObject skillSlotPrefab;
    [SerializeField] private Transform magicSkillsPanel;
    [SerializeField] private Transform weaponSkillsPanel;

    [Space(10)]
    [SerializeField] private Text timeText;

    WorldTime worldTime;
    List<SkillSlot> slots = new List<SkillSlot>();

    void Start()
    {
        worldTime = FindObjectOfType<WorldTime>();
        worldTime.OnTimeChangedEvent += SetTimeText;

        UIEventHandler.OnPlayerHealthChanged += UpdateHealthBar;
        UIEventHandler.OnPlayerMagickaChanged += UpdateMagickaBar;
        UIEventHandler.OnPlayerStaminaChanged += UpdateStaminaBar;
        UIEventHandler.OnPlayerXPChanged += UpdateExp;
        UIEventHandler.OnPlayerSkillsChanged += UpdateSkills;
    }

    public void UpdateHealthBar(int currentValue, int maxValue)
    {
        healthBar.fillAmount = 1f * currentValue / maxValue;
    }

    public void UpdateMagickaBar(int currentValue, int maxValue)
    {
        magickaBar.fillAmount = 1f * currentValue / maxValue;
    }

    public void UpdateStaminaBar(int currentValue, int maxValue)
    {
        staminaBar.fillAmount = 1f * currentValue / maxValue;
    }

    public void UpdateExp(int currentXP, int xpToNextLevel, int level)
    {
        expImage.fillAmount = 1f * currentXP / xpToNextLevel;
        lvlText.text = level.ToString();
    }

    public void UpdateSkills(CharacterSkills skills)
    {
        // Remove skill slots
        foreach(SkillSlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();

        // Add skill slots
        foreach(Skill skill in skills.Skills)
        {
            GameObject slot;

            if(skill is MagicSkill)
            {
                slot = Instantiate(skillSlotPrefab, magicSkillsPanel);
            }
            else
            {
                slot = Instantiate(skillSlotPrefab, weaponSkillsPanel);
            }

            SkillSlot skillSlot = slot.GetComponent<SkillSlot>();
            skillSlot.Skill = skill;
            if (skills.GetActiveSkills().Contains(skill))
                skillSlot.IsActive = true;

            slots.Add(skillSlot);
        }
    }

    void SetTimeText()
    {
        timeText.text = (worldTime.LocalTime / 60).ToString();
    }
}
