using System;
using Audio;
using DefaultNamespace;
using UnityEngine;


public class PlayerPickup : MonoBehaviour
{
    public enum PickupType
    {
        None,
        Key,
        Tablet,
        GoalRed,
        GoalBlue,
        GoalPurple
    }
    
    [SerializeField] SpriteRenderer pickupSprite;
    
    public PickupType pickedUp = PickupType.None;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (pickedUp == PickupType.Key)
            {
               var door = other.gameObject.GetComponent<Door>(); 
               door.Open();
               AudioManager.Instance.PlayOneShot(FmodEvents.Instance.GateOpen, door.transform.position);
               pickedUp = PickupType.None;
               pickupSprite.sprite = null;
            }
        }
    }

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
                      if (placement.isSpecial)
                      {
                          GameManager.Instance.CheckWinningCondition();
                      }
                      
                      AudioManager.Instance.PlayOneShot(FmodEvents.Instance.PlaceObject, placement.transform.position);
                  }
              }  
              else if (pickedUp == PickupType.None && placementSprite.sprite != null && !placement.isSpecial)
              {
                  var otherSprite = placement.RemoveSprite();
                  pickupSprite.sprite = otherSprite.sprite;
                  pickedUp = placement.placementType;
                  if (placement.isSpecial)
                  {
                      GameManager.Instance.CheckWinningCondition();
                  }
                
                  AudioManager.Instance.PlayOneShot(FmodEvents.Instance.BubblePickup, placement.transform.position);
              }
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
                    
                    AudioManager.Instance.PlayOneShot(FmodEvents.Instance.PlaceObject, other.transform.position);
                }
                else if (pickedUp == PickupType.None && socket.Active)
                {
                    Debug.Log("Player picked up none");
                    pickupSprite.sprite = socket.Disable().sprite;
                    pickedUp = PickupType.Tablet;
                    
                    AudioManager.Instance.PlayOneShot(FmodEvents.Instance.BubblePickup, other.transform.position);
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
                AudioManager.Instance.PlayOneShot(FmodEvents.Instance.BubblePickup, transform.position);
            

            }
        }
    }
}
