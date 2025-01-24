using System;
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
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Activate()
        {
            spriteRenderer.enabled = true;
            Active = true;
            movingObject.transform.DOMove(child2.transform.position, 0.5f);
        }

        public SpriteRenderer Disable()
        {
            spriteRenderer.enabled = false;
            Active = false;
            movingObject.transform.DOMove(child1.transform.position, 0.5f);
            return spriteRenderer;
        }

        private void Start()
        {
            spriteRenderer.enabled = Active;
            
            movingObject.transform.position = child1.transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(child1.transform.position, child2.transform.position);
        }
    }
}