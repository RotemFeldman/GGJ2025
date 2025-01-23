using System;
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
