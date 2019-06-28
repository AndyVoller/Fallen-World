using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Skill", menuName = "Skills/Weapon Skill")]
public class WeaponSkill : Skill
{
    [SerializeField] protected int staminaCost;
    public int StaminaCost { get { return staminaCost; } }

    public override bool Use(Transform user)
    {
        IWeapon sword = user.GetComponentInChildren<IWeapon>();
        if (sword == null)
            return false;

        CharacterStats stats = user.GetComponent<CharacterStats>();
        Animator animator = user.GetComponent<Animator>();
        Damage damage = new Damage(stats.GetStat("Attack").GetValue(), Element.Physical);

        if (stats.UseStamina(StaminaCost))              // If there is enough stamina to use skill
        {
            sword.Damage = damage;
            animator.SetTrigger("Attack");
            return true;
        }

        return false;
    }

}
