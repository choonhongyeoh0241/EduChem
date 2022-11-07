using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivation2 : BoxColliderActivation
{
    protected override void Update() 
    {
        if (playerInRange)
        {
            bridge.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("1st");
            Debug.Log("Walk on bridge now");
            foreach(var box in boxCollider2Ds)
            {
                box.enabled = true;
            }

            foreach(var box in tilesCollider)
            {
                box.enabled = false;
            }
        }
    }
}
