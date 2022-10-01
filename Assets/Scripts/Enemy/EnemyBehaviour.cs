using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 playerPosition;
    float timerMovement = 0;
    float timerAttack = 0;
    float playerRadius = 2f;
    bool isAttacking = false;
    Vector3 pointAroundPlayer;
    Vector3 targetPositionAttack;
    Vector3 targetPositionMovement;
    bool isInCooldown = false;
    float attackCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerMovement();
        if (isInCooldown){
            UpdateTimerAttack();
        }
        GetPlayerPosition();
    }

    void FixedUpdate()
    {
        SetTargetPositionMovement(2.0f);
        MakeAction();
    }

    void UpdateTimerMovement(){
        timerMovement += Time.deltaTime;
    }

    void UpdateTimerAttack(){
        timerAttack += Time.deltaTime;
        if (timerAttack >= attackCooldown){
            isInCooldown = false;
            timerAttack = 0;
        }
    }


    void GetPlayerPosition()
    {
        playerPosition = player.transform.position;
    }

    void SetTargetPositionMovement(float step){
        // Take a random point inside a radius around the enemy
        if (timerMovement >= step){
            pointAroundPlayer = Random.insideUnitCircle * playerRadius;
            timerMovement = 0;
        }
        targetPositionMovement = playerPosition + pointAroundPlayer;
    }

    void MakeAction(){
        if (isCloseToTarget()){
            if (!isInCooldown){
                Attack();
            }
        }
        else{
            Move(targetPositionMovement, 0.05f);
        }
    }

    bool isCloseToTarget(){
        bool isClose = false;
        float distance = Vector3.Distance(playerPosition, transform.position);
        if (distance < playerRadius){
            isClose = true;
        }

        return isClose;
    }

    void Attack(){
        if (!isAttacking){
            Vector3 headingVector = playerPosition - transform.position;
            targetPositionAttack = playerPosition + (Vector3.Normalize(headingVector) * 1.5f);
            isAttacking = true;
        }
        // Do a dash attack
        Move(targetPositionAttack, 0.5f);
        if (isAttacking && targetPositionAttack == transform.position){
            isAttacking = false;
            isInCooldown = true;
        }
    }

    void Move(Vector3 targetPosition, float velocity){
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, velocity);
        transform.position = newPosition;
    }
}
