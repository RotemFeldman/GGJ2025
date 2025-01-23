using System;
using DG.Tweening;
using UnityEngine;
using static DG.Tweening.DOTween;

public class StealthBubble : MonoBehaviour
{
    public Action OnPopped;
    
    [SerializeField] private float maxAir = 100f;
    [SerializeField] private float currentAir;
    [SerializeField] private float decayRate = 1f;
    [Range(0f, 1f)] float popThreshold = 0.2f;

    private float _airT;

    private bool isActive;
    private Vector3 _startScale;
    private Transform player;
    
    
    private void Start()
    {
        currentAir = maxAir;
        _startScale = transform.localScale;
    }

    private void Update()
    {
        if (isActive)
        {
            currentAir -= decayRate * Time.deltaTime;
            _airT = currentAir / maxAir;
            Vector3 newScale = Vector3.Lerp(Vector3.zero, _startScale, _airT); 
            transform.localScale = newScale;
            
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * (5 * Time.deltaTime);

            if (_airT <= popThreshold)
            {
                Pop();
            }
        }
        
    }

    public void Activate(Transform playerTransform)
    {
        isActive = true;
        player = playerTransform;
    }
    

    private void Pop()
    {
        OnPopped?.Invoke();
        Destroy(gameObject);
    }

    void PickedUp()
    {
        isActive = true;
    }
}
