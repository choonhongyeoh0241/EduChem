using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderActivation : MonoBehaviour
{
    [SerializeField] protected GameObject bridge;
    [SerializeField] protected BoxCollider2D[] boxCollider2Ds;
    [SerializeField] protected BoxCollider2D[] tilesCollider;
    protected bool playerInRange;

    private void Start() 
    {
        setBridge3rd();
    }
    protected virtual void Update() 
    {
        if (playerInRange)
        {
            setBridge3rd();
        }
    }

    private void setBridge3rd()
    {
        bridge.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("3rd");
        Debug.Log("Walk under bridge now");

            foreach(var box in boxCollider2Ds)
            {
                box.enabled = false;
            }

            foreach(var box in tilesCollider)
            {
                box.enabled = true;
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
