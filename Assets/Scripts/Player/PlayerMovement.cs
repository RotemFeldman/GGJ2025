using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private GameObject playerSprite;

    [Header("animating")]
    [SerializeField] private Vector3 squash;
    [SerializeField] private float animationSpeed;
    private int animDir = 1;
    private float animT = 1f;
    bool isMoving = false;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
            isMoving = true;
        
        var movement = context.ReadValue<Vector2>();
        rb.linearVelocity = movement * moveSpeed;

        AnimateWalk();

        if (context.canceled)
        {
            playerSprite.transform.localScale = Vector3.one;
            isMoving = false;
        }
    }

    private void Update()
    {
        AnimateWalk();
    }

    void AnimateWalk()
    {
        if (!isMoving)
            return;
        
        var scale = Vector3.Lerp(Vector3.one, squash, animT);
        playerSprite.transform.localScale = scale;

        if (animDir == 1)
        {
            animT -= Time.deltaTime * animationSpeed;
            if (animT <= 0)
                animDir *= -1;
        }
        else if (animDir == -1)
        {
            animT += Time.deltaTime * animationSpeed;
            if(animT >= 1)
                animDir *= -1;
        }
    }
}
