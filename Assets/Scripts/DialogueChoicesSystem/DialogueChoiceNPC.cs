using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceNPC : MonoBehaviour
{
    [SerializeField] private TextAsset dialogValue;
    private bool playerInRange; 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            DialogueChoiceManager.RequestChoiceDialog(dialogValue);
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
