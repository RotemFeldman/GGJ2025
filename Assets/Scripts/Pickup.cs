using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Sprite tablet;
        [SerializeField] private Sprite key;
        
        public SpriteRenderer spriteRenderer;
        public PlayerPickup.PickupType pickupType;

        private void Start()
        {
            if(pickupType == PlayerPickup.PickupType.Key)
                spriteRenderer.sprite = key;
            else if(pickupType == PlayerPickup.PickupType.Tablet)
            {
                spriteRenderer.sprite = tablet;
            }
        }
    }
}