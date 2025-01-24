using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BadBubble : MonoBehaviour
{
    public float Damage = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField] private float detectionDistance;
    [SerializeField] private float followDistance;
    [SerializeField] private float moveSpeed;
    
    private PlayerHealth player;
    private bool isAggroed;
    [SerializeField] public bool isPatrolling;
    [SerializeField] public List<Vector3> patrolPoints = new List<Vector3>();
    private int nextPatrolPoint = 0;
    private bool isAlerting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
    }

    private void Update()
    {
        
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= detectionDistance && !player.IsStealth)
        {
            if (!isAggroed && !isAlerting)
            {
                StartCoroutine(AlertAnimation());
            }
            
            isAggroed = true;
            
            if(!isAlerting)
                ChasePlayer();
        }
        else if (distance <= followDistance && isAggroed && !player.IsStealth && !isAlerting)
        {
            ChasePlayer();
        }
        else
        {
            isAggroed = false;
        }
        
        if (!isAggroed && isPatrolling)
        {
            Patrol();
        }
        else if (!isPatrolling)
        {
            _rigidbody.linearVelocity = Vector2.zero; 
        }
        
        _animator.SetBool("IsMoving", _rigidbody.linearVelocity.magnitude != 0);
        
    }

    private IEnumerator AlertAnimation()
    {
        isAlerting = true;
        
        yield return new WaitForSeconds(0.5f);
        
        isAlerting = false;
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
        _rigidbody.linearVelocity = direction * moveSpeed;
    }
    
    private void Patrol()
    {
        Vector3 target = patrolPoints[nextPatrolPoint];
        Vector3 direction = (target - transform.position).normalized;
        _rigidbody.linearVelocity = direction * moveSpeed;

        if (Vector2.Distance(target, transform.position) <= 0.1f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
        
    }
    
    
}
