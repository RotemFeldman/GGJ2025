using System;
using DefaultNamespace;
using UnityEngine;


public class PlayerPickup : MonoBehaviour
{
    public enum PickupType
    {
        None,
        Key,
        Goal,
        Tablet
    }
    
    [SerializeField] SpriteRenderer pickupSprite;
    
    public PickupType pickedUp = PickupType.None;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Placement"))
        {
            var placement = other.gameObject.GetComponent<PickupPlacement>();
            var placementSprite = placement.GetComponent<SpriteRenderer>();
            

            if (placement != null)
            {
              if (pickedUp != PickupType.None && placementSprite.sprite == null)
              {
                  if (placement.placementType == pickedUp)
                  {
                      pickedUp = PickupType.None;
                      pickupSprite.sprite = null;
                      placement.AddSprite();
                  }
              }  
            }
            else if (pickedUp == PickupType.None && placementSprite.sprite != null)
            {
                var otherSprite = placement.RemoveSprite();
                pickupSprite.sprite = otherSprite.sprite;
                pickedUp = placement.placementType;
            }
           
        }
        else if (other.gameObject.CompareTag("Socket"))
        {
            var socket = other.gameObject.GetComponent<Socket>();
            if (socket != null)
            {
                if (pickedUp == PickupType.Tablet && !socket.Active)
                {
                    Debug.Log("Player picked up tablet");
                    socket.Activate();
                    pickedUp = PickupType.None;
                    pickupSprite.sprite = null;
                }
                else if (pickedUp == PickupType.None && socket.Active)
                {
                    Debug.Log("Player picked up none");
                    pickupSprite.sprite = socket.Disable().sprite;
                    pickedUp = PickupType.Tablet;
                }
                                
            }
        }
        else if(pickedUp == PickupType.None)
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
