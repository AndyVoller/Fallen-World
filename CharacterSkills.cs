using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterStats))]
public class CharacterSkills : MonoBehaviour
{
    [SerializeField]
    protected List<Skill> skills = new List<Skill>();
    public List<Skill> Skills { get { return skills; } }

    // Only active skills can be used 
    public MagicSkill MainActiveMagicSkill { get; protected set; }
    public MagicSkill SecondActiveMagicSkill { get; protected set; }
    public WeaponSkill WeaponActiveSkill { get; protected set; }

    // Reduce cooldown time for magic skills
    public bool TwoHandsMagic { get; protected set; }

    public bool CanUseSkill { get; set; }

    void Awake()
    {

        MainActiveMagicSkill = skills[0] as MagicSkill;
        WeaponActiveSkill = skills[1] as WeaponSkill;

        TwoHandsMagic = false;
        CanUseSkill = true;
    }

    void Start()
    {
        UIEventHandler.PlayerSkillsChanged(this);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!CanUseSkill)
            return;

        if(GetComponent<EquipmentManager>().Weapon == null && !TwoHandsMagic)
        {
            TwoHandsMagic = true;
            SecondActiveMagicSkill = MainActiveMagicSkill;
        }

        if (Input.GetMouseButtonDown(0))            // Main magic skill
        {
            if (MainActiveMagicSkill.Use(this.transform))
            {
                CanUseSkill = false;
                if (TwoHandsMagic)                  // Reduce cooldown time
                    StartCoroutine(Cooldown(MainActiveMagicSkill.CastingTime * 1.0f / 150));
                else
                    StartCoroutine(Cooldown(MainActiveMagicSkill.CastingTime * 1.0f / 100));
            }
        }

        if (Input.GetMouseButtonDown(1))            // Second magic or weapon skill
        {
            if (TwoHandsMagic)
            {
                if (SecondActiveMagicSkill.Use(this.transform))
                {
                    CanUseSkill = false;
                    StartCoroutine(Cooldown(SecondActiveMagicSkill.CastingTime * 1.0f / 150));
                }
            }
            else
            {
                if (WeaponActiveSkill.Use(this.transform))
                {
                    CanUseSkill = false;
                    StartCoroutine(Cooldown(WeaponActiveSkill.CastingTime * 1.0f / 100));
                }
            }
        }

    }

    IEnumerator Cooldown(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        CanUseSkill = true;
    }

    public void SetSkill(Skill skill, bool isActive)
    {
        if (isActive)                     // Deactivate skill
        {
            if (skill is WeaponSkill) 
            {
                WeaponActiveSkill = null;
                TwoHandsMagic = true;
                SetSecondMagicSkill(MainActiveMagicSkill);
            }
            else
            {
                if(skill==SecondActiveMagicSkill)
                {
                    SecondActiveMagicSkill = MainActiveMagicSkill;
                }
                else
                {
                    if (TwoHandsMagic) 
                        MainActiveMagicSkill = SecondActiveMagicSkill;
                }
            }
        }
        else                            // Activate skill
        {
            if(skill is WeaponSkill)
            {
                SetWeaponSkill(skill as WeaponSkill);
            }
            else
            {
                if (TwoHandsMagic)
                {
                    SetSecondMagicSkill(skill as MagicSkill);
                }
                else
                {
                    SetMainMagicSkill(skill as MagicSkill);
                }
            }
        }
    }

    public void SetMainMagicSkill(MagicSkill skill)
    {
        if(skills.Contains(skill))
        {
            MainActiveMagicSkill = skill;
            UIEventHandler.PlayerSkillsChanged(this);
        }
    }

    public void SetSecondMagicSkill(MagicSkill skill)
    {
        if (skills.Contains(skill))
        {
            SecondActiveMagicSkill = skill;
            UIEventHandler.PlayerSkillsChanged(this);
        }
    }

    public void SetWeaponSkill(WeaponSkill skill)
    {
        if (skills.Contains(skill))
        {
            if (TwoHandsMagic)
            {
                TwoHandsMagic = false;
                SecondActiveMagicSkill = null;
            }

            WeaponActiveSkill = skill;
            UIEventHandler.PlayerSkillsChanged(this);
        }
    }

    public List<Skill> GetActiveSkills()
    {
        List<Skill> activeSkills = new List<Skill>();

        if (MainActiveMagicSkill != null)
            activeSkills.Add(MainActiveMagicSkill);
        if (SecondActiveMagicSkill != null)
            activeSkills.Add(SecondActiveMagicSkill);
        if (WeaponActiveSkill != null)
            activeSkills.Add(WeaponActiveSkill);

        return activeSkills;
    }

}
