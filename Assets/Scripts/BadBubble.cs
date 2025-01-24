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
    [SerializeField] private bool isPatrolling;
    [SerializeField] private List<Transform> patrolPoints;
    private int nextPatrolPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if(player.IsStealth)
            return;
        
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= detectionDistance)
        {
            isAggroed = false;
           ChasePlayer();
        }
        else if (distance <= followDistance && isAggroed)
        {
            ChasePlayer();
        }
        else
        {
            isAggroed = false;
        }

        if (!isAggroed && isPatrolling)
        {
            transform.position = Vector3.MoveTowards(patrolPoints[nextPatrolPoint].position, player.transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[nextPatrolPoint].position) <= 0.1f)
            {
                GoToNextPatrolPoint();
            }
        }
        
        
    }

    private void GoToNextPatrolPoint()
    {
        if (nextPatrolPoint < patrolPoints.Count - 1)
        {
            nextPatrolPoint++;
        }
        else
        {
            nextPatrolPoint = 0;
        }
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

        if (isPatrolling)
        {
            Gizmos.color = Color.white;

            for (int i = 0; i < patrolPoints.Count -1; i++)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i+1].position);
            }
            Gizmos.DrawLine(patrolPoints[patrolPoints.Count -1].position, patrolPoints[0].position);
        }
    }
}
