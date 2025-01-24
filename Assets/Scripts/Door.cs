using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;

    public void Open()
    {
        Destroy(gameObject);
    }
    
}
