using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Body will be attracted by gravitational force
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : MonoBehaviour
{
    public bool IsFallen { get; private set; }              // Game object is fallen now

    Rigidbody2D rb;
    Vector3 forceDirection;
    Gravitation gravitation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsFallen = false;
        gravitation = Gravitation.Instance;
    }

    void Update()
    {
        forceDirection = (gravitation.PlanetCenter - transform.position).normalized;

        float angle = Vector3.Angle(rb.velocity, forceDirection);
        if (angle > -90f && angle < 90f)
            IsFallen = true;
        else IsFallen = false;

        transform.up = -forceDirection;
    }

    void FixedUpdate()
    {
        // Fallen speed limit
        if (rb.velocity.magnitude < gravitation.MaxFallSpeed || !IsFallen)
            rb.AddForce(gravitation.GetForce(transform.position));
                // Don't use gravity if body is fallen and has max fallen speed
    }

}
