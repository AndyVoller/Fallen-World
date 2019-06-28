using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    [SerializeField] new protected string name;
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Element element;
    [SerializeField] protected int castingTime;         // Milliseconds

    public int CastingTime { get { return castingTime; } }
    public Sprite Icon { get { return icon; } }

    public virtual bool Use(Transform user)
    {
        return true;
    }
}
