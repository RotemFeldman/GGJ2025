using System;
using Audio;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Socket : MonoBehaviour
    {
        [SerializeField] private GameObject movingObject;

        [SerializeField] private GameObject child1;
        [SerializeField] private GameObject child2;
        
        public bool Active;
        [SerializeField] private GameObject movingObject2;
        [SerializeField] private GameObject child3;
        [SerializeField] private GameObject child4;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Activate()
        {
            spriteRenderer.enabled = true;
            Active = true;
            movingObject.transform.DOMove(child2.transform.position, 0.5f);
            
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.WallMove,movingObject.transform.position);
            
            if(movingObject2 != null)
                movingObject2.transform.DOMove(child4.transform.position, 0.5f);
        }

        public SpriteRenderer Disable()
        {
            spriteRenderer.enabled = false;
            Active = false;
            movingObject.transform.DOMove(child1.transform.position, 0.5f);
            
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.WallMove,movingObject.transform.position);
            
            if(movingObject2 != null)
                movingObject2.transform.DOMove(child3.transform.position, 0.5f);
            
            return spriteRenderer;
        }

        private void Start()
        {
            spriteRenderer.enabled = Active;
            
            movingObject.transform.position = child1.transform.position;
            
            if(movingObject2 != null)
                movingObject2.transform.position = child3.transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(child1.transform.position, child2.transform.position);
        }
    }
}