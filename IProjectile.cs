using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    Vector3 MoveDirection { get; set; }
    Damage Damage { get; set; }
}
