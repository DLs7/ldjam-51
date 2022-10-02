using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    void Awake()
    {
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.tag);
        if(col.tag == "Enemy"){
            Destroy(this.gameObject);
        }
    }
}
