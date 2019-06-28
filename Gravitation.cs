using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class is used to create simple gravitation around planet
/// </summary>
public class Gravitation : MonoBehaviour
{
    #region Singleton

    public static Gravitation Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            if(Instance!=this)
            {
                Debug.LogWarning("There is more than one instance of Gravitation");
                Destroy(this.gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public Vector3 PlanetCenter { get; private set; }
    public float Acceleration { get; private set; }     // Gravitational acceleration
    public float MaxFallSpeed { get; private set; }         // Free fall speed limit

    void Start ()
    {
        PlanetCenter = Vector3.zero;
        Acceleration = 10f;
        MaxFallSpeed = 20f;
	}

    public Vector3 GetForceDirection(Vector3 bodyPosition)
    {
        return PlanetCenter - bodyPosition;
    }
	
    public Vector3 GetForce(Vector3 bodyPosition)
    {
        return (PlanetCenter - bodyPosition).normalized * Acceleration;
    }
}
