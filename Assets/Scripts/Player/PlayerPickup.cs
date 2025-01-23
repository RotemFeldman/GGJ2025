using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] SpriteRenderer pickupSprite;
    
    private bool pickedUp = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            pickedUp = true;
            var pickup = other.gameObject.GetComponent<Pickup>();
            pickupSprite.sprite = pickup.spriteRenderer.sprite;
            pickupSprite.color = pickup.spriteRenderer.color;
            
            Destroy(other.gameObject);
        }
    }
}
