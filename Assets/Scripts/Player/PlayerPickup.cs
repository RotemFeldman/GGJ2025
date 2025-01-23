using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public enum PickupType
    {
        None,
        Key,
        Goal
    }
    
    [SerializeField] SpriteRenderer pickupSprite;
    
    public PickupType pickedUp = PickupType.None;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(pickedUp == PickupType.None)
            if (other.gameObject.CompareTag("Pickup"))
            {
                var pickup = other.gameObject.GetComponent<Pickup>();
                pickedUp = pickup.pickupType;
                pickupSprite.sprite = pickup.spriteRenderer.sprite;
                pickupSprite.color = pickup.spriteRenderer.color;
            
                Destroy(other.gameObject);
            }
    }
}
