using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Magic Skill", menuName = "Skills/Magic Skill")]
public class MagicSkill : Skill
{
    [SerializeField] protected int magickaCost;
    public int MagickaCost { get { return magickaCost; } }

    [SerializeField] protected GameObject projectileToSpawn;

    public override bool Use(Transform user)
    {
        CharacterStats stats = user.GetComponent<CharacterStats>();
        if (!stats.UseMagicka(MagickaCost))             // If there is not enough magicka to use skill
        {
            return false;
        }

        Animator animator = user.GetComponent<Animator>();
        // animator.SetTrigger("MagicAttack");

        // Projectile properties
        Vector3 spawnPosition = user.transform.Find("SpawnPosition").position;
        Vector3 direction = user.right;
        bool facingRight = user.GetComponent<CharacterController2D>().FacingRight;
        direction = facingRight ? direction : -direction;
        Damage damage = new Damage(stats.GetStat(element.ToString() + " Attack").GetValue(), element);

        SpawnProjectile(spawnPosition, direction, damage);
        return true;
    }

    void SpawnProjectile(Vector3 spawnPosition, Vector3 direction, Damage damage)
    {
        GameObject projectile = Instantiate(projectileToSpawn, spawnPosition, Quaternion.identity);
        IProjectile iprojectile = projectile.GetComponent<IProjectile>();
        iprojectile.MoveDirection = direction;
        iprojectile.Damage = damage;
    }

}
