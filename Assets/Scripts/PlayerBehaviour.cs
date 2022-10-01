using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D Rigidbody2D;
    private Vector2 MovementDirection;
    
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
    }

    void Move()
    {
        Rigidbody2D.velocity = new Vector2(MovementDirection.x * MovementSpeed, MovementDirection.y * MovementSpeed);
    }
}
