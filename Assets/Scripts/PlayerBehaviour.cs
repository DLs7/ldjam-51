using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public TrailRenderer TrailRenderer;
    
    private Vector2 GoBackInTimePosition;
    private bool GoBackInTimePositionSet;

    private Vector3[] Waypoints;
    private int WaypointIndex;
    private bool GoingBackInTime;
    
    private float GoBackInTimeTimer = 10f;
    
    private void Start()
    {
        ActiveMovementSpeed = MovementSpeed;
    }

    void Update()
    {
        if(!GoingBackInTime) GetInputs();
    }

    void FixedUpdate()
    {
        if (GoingBackInTime)
        {
            GoBackInTime();
        }
        else
        {
            SetBackInTime();
            Move();
        }
    }

    void SetBackInTime()
    {
        GoBackInTimeTimer -= Time.fixedDeltaTime;
        
        if (GoBackInTimeTimer <= 5 && !GoBackInTimePositionSet )
        {
            GoBackInTimePosition = transform.position;
            GoBackInTimePositionSet = true;

            TrailRenderer.enabled = true;
        }

        if (GoBackInTimeTimer <= 0)
        {
            var positionCount = TrailRenderer.positionCount;
            
            WaypointIndex = positionCount - 1;
            Waypoints = new Vector3[positionCount];
            TrailRenderer.GetPositions(Waypoints);
            
            // TrailRenderer.enabled = false;
            
            GoingBackInTime = true;
        }
    }

    void GoBackInTime()
    {
        if (Waypoints[WaypointIndex] == transform.position)
        {
            WaypointIndex--;
        }

        transform.position = Vector3.MoveTowards(transform.position, Waypoints[WaypointIndex], Time.time);

        if (WaypointIndex == 0)
        {
            GoBackInTimeTimer = 10f;
            GoBackInTimePositionSet = false;
            GoingBackInTime = false;
        }
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
