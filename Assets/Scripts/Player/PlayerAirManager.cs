using System;
using UnityEngine;

public class PlayerAirManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual;

    [SerializeField] private float maxAir = 100f;
    [SerializeField] private float currentAir;
    [SerializeField] private float decayRate = 1f;

    private float _airT;
    
    [SerializeField] Vector3 maxScale = Vector3.one;
    [SerializeField] Vector3 minScale = Vector3.zero;

    private void Start()
    {
        currentAir = maxAir;
    }

    private void Update()
    {
        currentAir -= decayRate * Time.deltaTime;
        _airT = currentAir / maxAir;
        
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, _airT);
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
    }
}
