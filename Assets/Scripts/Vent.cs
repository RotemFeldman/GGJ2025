using System;
using DG.Tweening;
using UnityEngine;
using static DG.Tweening.DOTween;

public class Vent : MonoBehaviour
{
    [SerializeField] public GameObject child;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.DOMove(transform.position, 0.3f).OnComplete((() =>
            {
                other.transform.DOMove(child.transform.position, 0.5f);
            }));
        }
    }*/

    public void MovePlayer(GameObject player)
    {
        player.transform.DOMove(transform.position, 0.3f).OnComplete((() =>
        {
            player.transform.DOMove(child.transform.position, 0.5f);
        }));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(child.transform.position, 0.3f);
        Gizmos.DrawLine(transform.position, child.transform.position);
    }
}
