﻿using UnityEngine;

/// <summary>
/// The Player's contol class.
/// </summary>

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Contains tunable parameters to tweak the player's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Space(10)]
        [Tooltip("The player's current speed.")]
        public float speed;

        [Tooltip("The fastest speed the player can go.")]
        public float speedMaximum;

        [Tooltip("The slowest speed the player can drop to.")]
        public float speedMinimum;

        [Tooltip("How fast the player turns left and right.")]
        public float turnSpeed;

        [Tooltip("How fast player speeds up.")]
        public float acceleration;

        [Tooltip("How fast player speeds down.")]
        public float deceleration;

        [Tooltip("Maximal accelerate.")]
        public float maxAcceleration;

        [Tooltip("Minimum accelerate.")]
        public float minAcceleration;

    }

    public Stats playerStats;

    [Tooltip("Keyboard controls for steering left and right.")]
    public KeyCode leftKey, rightKey;

    [Tooltip("Keyboard controls for acceleration and deceleration.")]
    public KeyCode accelerationKey, decelerationKey;

    [Tooltip("Whether the player is moving downhill or not.")]
    public bool isMoving;

    [Tooltip("Child GameObject to check if we are on the ground")]
    private GroundCheck groundCheck;

    private Rigidbody rb;

    private Animator animator;

    private PlayerDamage playerDamage;



    private void Start()
    {
        // grabs references to components.
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        playerDamage = GetComponent<PlayerDamage>();

    }

    private void Update()
    {
        if (isMoving && groundCheck.isGrounded && !playerDamage.isHurt)
        {
            if (Input.GetKey(leftKey))
            {
                TurnLeft();
            }
            if (Input.GetKey(rightKey))
            {
                TurnRight();
            }
            if (Input.GetKey(accelerationKey))
            {
                Acceleration();
            }
            if (Input.GetKey(decelerationKey))
            {
                Deceleration();
            }
        }
    }

    private void FixedUpdate()
    {
        ControlSpeed();
        ControlAcceleration();

        if (isMoving && !playerDamage.isHurt)
        {
            // increase or decrease the players speed depending on how much they are facing downhill.
            float turnAngle = Mathf.Abs(180 - transform.eulerAngles.y);
            playerStats.speed += Remap(0, 90, playerStats.acceleration, -playerStats.deceleration, turnAngle);

            // moves the player forward based on which direction they are facing.
            Vector3 velocity = (transform.forward) * playerStats.speed * Time.fixedDeltaTime;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        // update the Animator's state depending on our speed.
        animator.SetFloat("playerSpeed", playerStats.speed);


    }

    private void TurnLeft()
    {
        // rotates the player, limiting them after reaching a certain angle.
        if (transform.eulerAngles.y < 269)
        {
            transform.Rotate(new Vector3(0, playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
        }
    }

    private void TurnRight()
    {
        if (transform.eulerAngles.y > 91)
        {
            transform.Rotate(new Vector3(0, -playerStats.turnSpeed, 0) * Time.deltaTime, Space.Self);
        }
    }

    private void Acceleration()
    {
        playerStats.acceleration++;  
    }

    private void Deceleration()
    {
        playerStats.acceleration--;
    }

    private void ControlAcceleration() 
    {
        if (playerStats.acceleration > playerStats.maxAcceleration)
        {
            playerStats.acceleration = playerStats.maxAcceleration;
        }
        if (playerStats.acceleration < playerStats.minAcceleration)
        {
            playerStats.acceleration = playerStats.minAcceleration;
        }
    }


    private void ControlSpeed()
    {

        // limits the player's speed when reaching past the speed maximum.
        if (playerStats.speed > playerStats.speedMaximum)
        {
            playerStats.speed = playerStats.speedMaximum;
        }

        // limits the player from moving any slower than the speed minimum.
        if (playerStats.speed < playerStats.speedMinimum)
        {
            playerStats.speed = playerStats.speedMinimum;
            
        }

    }


    /// <summary>
    /// Remaps a number from a given range into a new range.
    /// </summary>
    /// <param name="OldMin"></param>
    /// <param name="OldMax"></param>
    /// <param name="NewMin"></param>
    /// <param name="NewMax"></param>
    /// <param name="OldValue"></param>
    /// <returns></returns>
    private float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}