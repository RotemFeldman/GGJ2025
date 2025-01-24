using System;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    public bool IsStealth;
    [SerializeField] private GameObject _playerVisual;

    [SerializeField] private float maxAir = 100f;
    [SerializeField] private float currentAir;
    [SerializeField] private float decayRate = 1f;

    [FormerlySerializedAs("_airT")] public float AirT;
    
    [SerializeField] Vector3 maxScale = Vector3.one;
    [SerializeField] Vector3 minScale = Vector3.zero;
    
    private EventInstance musicInstance;

    private void Start()
    {
        currentAir = maxAir;
    }

    private void Update()
    {
        currentAir -= decayRate * Time.deltaTime;
        AirT = currentAir / maxAir;
        
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, AirT);
        _playerVisual.transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoodBubble"))
        {
            var gains = other.gameObject.GetComponent<GoodBubble>().AirAmount;
            currentAir += gains;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BadBubble"))
        {
            var lose = other.gameObject.GetComponent<BadBubble>().Damage;
            currentAir -= lose;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("StealthBubble"))
        {
            var bubble = other.gameObject.GetComponent<StealthBubble>();
            bubble.OnPopped += StealthBubblePopped;
            IsStealth = true;
            bubble.Activate(transform);
        }
    }
    

    void StealthBubblePopped()
    {
        IsStealth = false;
    }
}
