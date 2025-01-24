using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;using UnityEngine.Events;
public class PickupPlacement : MonoBehaviour
{
    [SerializeField] public PlayerPickup.PickupType placementType;
    [SerializeField] private SpriteRenderer placedSprite;
    [SerializeField] private SpriteRenderer pickUpDisplay;

    public void AddSprite()
    {
        placedSprite.sprite = pickUpDisplay.sprite;
        placedSprite.color = pickUpDisplay.color;
    }

    public SpriteRenderer RemoveSprite()
    {
        placedSprite.sprite = null;
        return pickUpDisplay;
    }
}
