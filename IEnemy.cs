using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void TakeDamage(Damage damage);
    void PerformAttack();
    void Die();
}
