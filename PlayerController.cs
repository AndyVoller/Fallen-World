using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    bool jump;
    bool dodge = false;
    float horizontalMove = 0f;
    float runSpeed = 50f;

    CharacterController2D characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    }

    void FixedUpdate()
    {
        if (horizontalMove == 0 && !jump)
        {
            characterController.Stop();
            return;
        }

        characterController.Move(horizontalMove * Time.fixedDeltaTime, dodge, jump);
        jump = false;
    }
}
