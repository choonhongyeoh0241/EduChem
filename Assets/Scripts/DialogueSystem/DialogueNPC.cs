using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private DialogueData firstDialogue;
    [SerializeField] private DialogueData secondDialogue;
    [SerializeField] private GameObject Bubble;
    [SerializeField] private BookData collectedBook;
    [SerializeField] private Inventory inventory;
    private bool playerInRange; 
    private bool bookExists;
    
    private void Start() 
    {
        if(Bubble==null)return;

        Bubble.SetActive(false);
    }
    private void Update()
    {
        checkCollectedBook();

        if (!bookExists)
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                Bubble.SetActive(false);
                DialogueManager.RequestDialogue(firstDialogue);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                Bubble.SetActive(false);
                DialogueManager.RequestDialogue(secondDialogue);
            }
        }
        
    }

    private void checkCollectedBook()
    {
        for (int i = 0; i < inventory.books.Count; i++)
        {
            if (inventory.Contains(collectedBook))
            {
                bookExists = true;
            }
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
