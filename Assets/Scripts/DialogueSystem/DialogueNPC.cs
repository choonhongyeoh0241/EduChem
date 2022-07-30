using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private GameObject Bubble;
    private bool playerInRange; 
    
    private void Start() 
    {
        if(Bubble==null)return;

        Bubble.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Bubble.SetActive(false);
            DialogueManager.RequestDialogue(dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            Bubble.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            Bubble.SetActive(false);
            playerInRange = false;
            DialogueManager.RequestDialogue(null);
        }
    }
}
