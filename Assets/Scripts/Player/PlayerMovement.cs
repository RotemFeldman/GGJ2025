using System;
using Audio;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] private GameObject playerSprite;

        [Header("animating")]
        [SerializeField] private Vector3 squash;
        [SerializeField] private float animationSpeed;
        private int animDir = 1;
        private float animT = 1f;
        bool isMoving = false;
    
        [Header("Audio")]
        private EventInstance playerWalkAudio;
    
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

        private void Start()
        {
            playerWalkAudio = AudioManager.Instance.CreateInstance(FmodEvents.Instance.PlayerWalkLoop);
        }

        private void Update()
        {
            AnimateWalk();
        
            UpdateSound();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Vent"))
            {
                var vent = other.gameObject.GetComponent<Vent>();
                vent.MovePlayer(gameObject);
            }
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
                {
                    animDir *= -1;
                }
                
            }
            else if (animDir == -1)
            {
                animT += Time.deltaTime * animationSpeed;
                if(animT >= 1)
                    animDir *= -1;

            }
        }

        void UpdateSound()
        {
            // walking sound
            if (isMoving)
            {
                PLAYBACK_STATE playbackState;
                playerWalkAudio.getPlaybackState(out playbackState);
                if (playbackState == PLAYBACK_STATE.STOPPED)
                {
                    playerWalkAudio.start();
                }
            }
            else
            {
                playerWalkAudio.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
