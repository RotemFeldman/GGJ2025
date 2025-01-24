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
        if (other.gameObject.CompareTag("Placement"))
        {
            var placement = other.gameObject.GetComponent<PickupPlacement>();
            var placementSprite = placement.GetComponent<SpriteRenderer>();
            if (pickedUp != PickupType.None && placementSprite.sprite == null)
            {
                if (placement.placementType == pickedUp)
                {
                    pickedUp = PickupType.None;
                    pickupSprite.sprite = null;
                    placement.AddSprite();
                }
            }
            else if (pickedUp == PickupType.None && placementSprite.sprite != null)
            {
                var otherSprite = placement.RemoveSprite();
                pickupSprite.sprite = otherSprite.sprite;
                pickedUp = placement.placementType;
            }
        }
        if(pickedUp == PickupType.None)
        {
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
}
