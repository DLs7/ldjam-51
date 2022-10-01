using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float Damping;

    private Vector3 Velocity = Vector3.zero;
    private void FixedUpdate()
    {
        if (transform.position != Target.position)
        {
            Vector3 targetPosition = new Vector3(Target.position.x + Damping, Target.position.y + Damping, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, Damping);
        }
    }
}
