using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BadBubble : MonoBehaviour
{
    public float Damage = 10f;
    
    [SerializeField] private float detectionDistance;
    [SerializeField] private float followDistance;
    [SerializeField] private float moveSpeed;
    
    private PlayerHealth player;
    private bool isAggroed;
    [SerializeField] public bool isPatrolling;
    [SerializeField] public List<Vector3> patrolPoints = new List<Vector3>();
    private int nextPatrolPoint = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
    }

    private void Update()
    {
        
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= detectionDistance && !player.IsStealth)
        {
            isAggroed = true;
           ChasePlayer();
        }
        else if (distance <= followDistance && isAggroed && !player.IsStealth)
        {
            ChasePlayer();
        }
        else
        {
            isAggroed = false;
        }
        
        if (!isAggroed && isPatrolling)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[nextPatrolPoint], moveSpeed * Time.deltaTime);
                    
            if (Vector2.Distance(patrolPoints[nextPatrolPoint],transform.position) <= 0.01f)
            {
                GoToNextPatrolPoint();
            }
        }
        
    }

    private void GoToNextPatrolPoint()
    {
        nextPatrolPoint++;
        if(nextPatrolPoint == patrolPoints.Count)
            nextPatrolPoint = 0;
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
        
    }
    
    
}
