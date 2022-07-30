using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderActivation : MonoBehaviour
{
    [SerializeField] protected GameObject bridge;
    [SerializeField] protected BoxCollider2D[] boxCollider2Ds;
    [SerializeField] protected BoxCollider2D[] tilesCollider;
    protected bool playerInRange;

    protected virtual void Update() 
    {
        if (playerInRange)
        {
            bridge.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("3rd");

            foreach(var box in boxCollider2Ds)
            {
                box.enabled = false;
            }

            foreach(var box in tilesCollider)
            {
                box.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
