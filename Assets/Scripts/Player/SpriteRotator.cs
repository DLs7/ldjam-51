using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    public Quaternion DesiredRotation = Quaternion.identity;

    void LateUpdate () {
        transform.rotation = DesiredRotation;
    }
}
