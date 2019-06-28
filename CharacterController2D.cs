using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float JumpForce { get; set; }
    public bool CanJump { get; set; }

    [Range(0, 1)]
    [SerializeField] private float stopSmoothing = 0.85f;
    [SerializeField] private LayerMask whatIsGround;

    private Vector2 up;                                  // Local directional vectors
    private Vector2 right;

    Transform groundCheck;
    new Rigidbody2D rigidbody;
    const float movementSmoothing= 0.05f;
    const float groundedRadius = 0.05f;
    Vector3 velocity = Vector3.zero;
    bool grounded = true;

    public bool FacingRight { get; set; }               // Determines which way the player is currently facing

    void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        FacingRight = false;
    }

    void Start()
    {
        JumpForce = 250f;
        CanJump = true;
    }

    void Update()
    {
        up = transform.up;
        right = transform.right;
    }

    void FixedUpdate()
    {
        grounded = false;
        CanJump = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                grounded = true;
                CanJump = true;
            }
        }
    }

    public void Move(float move, bool dodge, bool jump)
    {
        up = transform.up;
        right = transform.right;

        if (HorizontalSpeed() > MaxSpeed)           // Limits character's velocity
        {
            if (move > 0f && FacingRight)
                move = 0f;
            if (move < 0f && !FacingRight)
                move = 0f;
        }

        Vector3 targetVelocity = right * move * 10f + up * VerticalSpeed();

        if (!grounded)                  // Can't to control character in the air
        {
            Vector3 inertia = right * HorizontalSpeed();
            if (FacingRight)
                targetVelocity += inertia;
            else
                targetVelocity -= inertia;
        }

        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing, MaxSpeed);

        // Flip charcter if moving direction is changing
        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }

        if (grounded && jump && CanJump)
        {
            // Add a vertical force to the player.
            grounded = false;
            rigidbody.AddForce(up * JumpForce);
        }
    }

    public void Stop()
    {
        if (!grounded)
            return;

        rigidbody.velocity *= stopSmoothing; 
    }

    // Local horizontal component of the speed vector
    float HorizontalSpeed()
    {
        // Scalar product  ( horizontal speed = |(rb.velocity, right)| )     
        float horizontalSpeed = rigidbody.velocity.x * right.x + rigidbody.velocity.y * right.y;
        horizontalSpeed = Mathf.Abs(horizontalSpeed);
        return horizontalSpeed;
    }

    // Local vertical component of the speed vector
    float VerticalSpeed()
    {
        // Scalar product  ( vertical speed = |(rb.velocity, up)| )
        float verticalSpeed = rigidbody.velocity.x * up.x + rigidbody.velocity.y * up.y;
        return verticalSpeed;
    }

    // Changing look direction
    void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        
        if(grounded)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
