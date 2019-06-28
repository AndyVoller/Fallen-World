using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IProjectile
{
    public Vector3 MoveDirection { get; set; }
    public Damage Damage { get; set; }

    Vector3 startPosition;
    static float speed = 4f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += MoveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, startPosition) > 5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<CharacterStats>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
