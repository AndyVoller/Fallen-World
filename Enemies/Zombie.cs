using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IEnemy
{
    private float aggroDistance = 3f;
    private float chaseDistance = 5f;
    private float attackDistance = 0.75f;

    Transform target;
    CharacterStats characterStats;
    CharacterController2D characterController;

    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        characterController = GetComponent<CharacterController2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        SetStats();                         // Make some difference between the same type enemies
    }

    void SetStats()
    {
        float delta = NormalDistribution.GetNum(0f, 0.2f);
        delta = Mathf.Clamp(delta, -0.25f, 0.25f);

        // Make difference in enemies' dimensions
        transform.localScale += new Vector3(delta, delta, 0f);

        // Change main stats
        characterStats.SetMaxHealth(Mathf.CeilToInt(characterStats.GetMaxHealth() * delta));
        characterStats.SetMaxMagicka(Mathf.CeilToInt(characterStats.GetMaxMagicka() * delta));
        characterStats.SetMaxStamina(Mathf.CeilToInt(characterStats.GetMaxStamina() * delta));
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < aggroDistance)
        {
            ChaseTarget(distance);
        }
        else if (distance > chaseDistance)
        {
            characterController.Stop();
        }
    }

    void ChaseTarget(float distance)
    {
        if (distance > attackDistance)
        {
            if (IsInvoking("PerformAttack"))
                CancelInvoke("PerformAttack");

            float move = GetTargetDirection();
            characterController.Move(move, false, false);
        }
        else
        {
            characterController.Stop();
            if (!IsInvoking("PerformAttack"))
                InvokeRepeating("PerformAttack", 0.5f, 1f);
        }
    }

    float GetTargetDirection()
    {
        Vector3 planetCenter = Vector3.zero;
        if (Vector3.SignedAngle(transform.position - planetCenter, target.position - planetCenter, Vector3.forward) < 0)
            return 1;
        else return -1;
    }

    public void PerformAttack()
    {
        // Simple implementation
            // Cool attack animation will be added later
        Damage damage = new Damage(characterStats.GetStat("Attack").GetValue(), Element.Physical);
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    public void TakeDamage(Damage damage)
    {
        characterStats.TakeDamage(damage);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
