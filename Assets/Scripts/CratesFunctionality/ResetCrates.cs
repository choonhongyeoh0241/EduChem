using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResetCrates : MonoBehaviour
{
    [SerializeField] private List<GameObject> crates;
    private bool playerInRange;

    private void Update() 
    {
        for (int i = 0; i < crates.Count; i++)
        {
            if (crates[i].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            {
                crates.Remove(crates[i]);
            }
        }
        if (playerInRange)
        {
            foreach(var crate in crates)
            {
                for (int i = 0; i < crates.Count * 5; i+=20)
                {
                    crate.transform.position = new Vector3 (i, 0, 0);
                }
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
            DialogueChoiceManager.RequestChoiceDialog(null);
        }
    }
}
