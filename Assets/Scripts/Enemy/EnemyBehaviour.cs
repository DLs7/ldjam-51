using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;
        Debug.Log(playerPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.02f);
    }
}
