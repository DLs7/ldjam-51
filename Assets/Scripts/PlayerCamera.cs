using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Target;
    public float Damping;

    private void FixedUpdate()
    {
        if (transform.position != Target.position)
        {
            Vector3 targetPosition = new Vector3(Target.position.x, Target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Damping);
        }
    }
}
