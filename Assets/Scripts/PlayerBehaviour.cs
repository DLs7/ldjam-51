using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D Rigidbody2D;
    private Vector2 MovementDirection;

    private float ActiveMovementSpeed;
    public float DashSpeed;
    public float DashLength = .5f, DashCooldown = 1f;

    private float DashCounter;
    private float DashCoolCounter;

    private void Start()
    {
        ActiveMovementSpeed = MovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInputs()
    {
        MovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashCounter <= 0 && DashCoolCounter <= 0)
            {
                ActiveMovementSpeed = DashSpeed;
                DashCounter = DashLength;
            }
        }
    }

    void Move()
    {
        Rigidbody2D.velocity = new Vector2(MovementDirection.x * ActiveMovementSpeed, MovementDirection.y * ActiveMovementSpeed);

        if (DashCounter > 0)
        {
            DashCounter -= Time.fixedDeltaTime;
            if (DashCounter <= 0)
            {
                ActiveMovementSpeed = MovementSpeed;
                DashCoolCounter = DashCooldown;
            }
        }

        if (DashCoolCounter > 0)
        {
            DashCoolCounter -= Time.fixedDeltaTime;
        }
    }
}
