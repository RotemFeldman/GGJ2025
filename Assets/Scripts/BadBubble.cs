using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using DG.Tweening;
using UnityEngine;

public class BadBubble : MonoBehaviour
{
    public float Damage = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _markAnimator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _faceRenderer;
    public Sprite[] faces;
    
    [SerializeField] private float detectionDistance;
    [SerializeField] private float followDistance;
    [SerializeField] private float moveSpeed;
    
    private PlayerHealth player;
    private bool isAggroed;
    [SerializeField] public bool isPatrolling;
    [SerializeField] public List<Vector3> patrolPoints = new List<Vector3>();
    private int nextPatrolPoint = 0;
    private bool isAlerting = false;
    private bool wait;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        _markAnimator.gameObject.SetActive(false);
        _faceRenderer.sprite = faces[0];
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Vent"))
        {
            transform.DOMove(other.transform.position, 0.3f).onComplete = () =>
            {
                Destroy(gameObject);
            };
        }
    }

    private void Update()
    {

        if (wait)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }
        
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
            _faceRenderer.sprite = faces[0];
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
        wait = true;
        
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.EnemyDetect, transform.position);
        _markAnimator.gameObject.SetActive(true);
        _faceRenderer.sprite = faces[1];
        
        yield return new WaitForSeconds(0.5f);
        wait = false;
        _markAnimator.gameObject.SetActive(false);
        StartCoroutine(AnimateFaceChasing());
        
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

    private IEnumerator AnimateFaceChasing()
    {
        if (isAggroed)
        {
            _faceRenderer.sprite = faces[3];
            yield return new WaitForSeconds(0.15f);
            _faceRenderer.sprite = faces[2];
            yield return new WaitForSeconds(0.15f);
            StartCoroutine(AnimateFaceChasing());
        }
        
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
