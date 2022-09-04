using UnityEngine;
using System.Collections.Generic;

public class BookRestrict : MonoBehaviour
{
    [SerializeField] private List<BookData> requiredBook;
    [SerializeField] private Inventory inventory;
    [SerializeField] private DialogueData dialogue;
    private bool playerInRange;

    private void Update() 
    {
        checkBook();
        
        if (requiredBook.Count == 0) 
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                DialogueManager.RequestDialogue(dialogue);
            }
            return;
        }
    }

    private void checkBook()
    {
        for (int i = 0; i < requiredBook.Count; i++)
        {
            if (inventory.Contains(requiredBook[i]))
            {
                requiredBook.Remove(requiredBook[i]);
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
            DialogueManager.RequestDialogue(null);
        }
    }

}
