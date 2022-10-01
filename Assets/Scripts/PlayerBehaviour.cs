using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerBehaviour : MonoBehaviour
{
    // Base movement variables
    public float MovementSpeed;
    public Rigidbody2D Rigidbody2D;
    private Vector2 MovementDirection;

    // Dash variables
    private float _activeMovementSpeed;
    public float DashSpeed;
    public float DashLength = 0.5f, DashCooldown = 1f;
    private float _dashCounter;
    private float _dashCoolCounter;

    // Go back in time variables
    public TrailRenderer TrailRenderer;
    private bool _goBackInTimePositionSet;
    // private Vector3[] _waypoints;
    // private int _waypointIndex;
    private bool _goingBackInTime;
    private Vector2 _goBackInTimePosition;
    private float _goBackInTimeTimer = 10f;

    private Vector2 _timeshiftSpeed = Vector2.zero;
    public float LerpTime = 0.75f;
    private float _currentLerpTime = 0f;
    private void Start()
    {
        _activeMovementSpeed = MovementSpeed;
    }

    void Update()
    {
        if(!_goingBackInTime) GetInputs();
    }

    void FixedUpdate()
    {
        if (_goingBackInTime)
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
        _goBackInTimeTimer -= Time.fixedDeltaTime;
        
        if (_goBackInTimeTimer <= 5 && !_goBackInTimePositionSet )
        {
            _goBackInTimePosition = Rigidbody2D.position;
            _goBackInTimePositionSet = true;
            TrailRenderer.enabled = true;
        }

        if (_goBackInTimeTimer <= 0)
        {
            _goingBackInTime = true;
        }
    }

    void GoBackInTime()
    {
        _currentLerpTime += Time.deltaTime;
        if (_currentLerpTime > LerpTime)
        {
            _currentLerpTime = LerpTime;
        }
        
        float xAxis = _currentLerpTime / LerpTime;
        Vector2 position = Vector2.Lerp(Rigidbody2D.position, _goBackInTimePosition, xAxis);
        Rigidbody2D.MovePosition(position);

        if (_goBackInTimePosition == Rigidbody2D.position)
        {
            _goBackInTimeTimer = 10f;
            _goBackInTimePositionSet = false;
            _goingBackInTime = false;
            _currentLerpTime = 0f;

            TrailRenderer.Clear();
            TrailRenderer.enabled = false;
        }
        
    }

    void GetInputs()
    {
        MovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dashCounter <= 0 && _dashCoolCounter <= 0)
            {
                _activeMovementSpeed = DashSpeed;
                _dashCounter = DashLength;
            }
        }
    }

    void Move()
    {
        Rigidbody2D.velocity = MovementDirection * _activeMovementSpeed;

        if (_dashCounter > 0)
        {
            _dashCounter -= Time.fixedDeltaTime;
            if (_dashCounter <= 0)
            {
                _activeMovementSpeed = MovementSpeed;
                _dashCoolCounter = DashCooldown;
            }
        }

        if (_dashCoolCounter > 0)
        {
            _dashCoolCounter -= Time.fixedDeltaTime;
        }
    }
}
