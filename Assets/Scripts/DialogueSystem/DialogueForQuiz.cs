using UnityEngine;

public class DialogueForQuiz : MonoBehaviour
{
    [SerializeField] private DialogueData dialogue;
    private bool playerInRange;
    private bool quizExists;
    private QuizNPC quizNPC;

    private void Start() 
    {
        quizNPC = this.GetComponent<QuizNPC>();
    }

    private void Update() 
    {
        checkQuizExists();
        
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (quizExists)
            {
                DialogueManager.RequestDialogue(null);
            }
            else
            {
                DialogueManager.RequestDialogue(dialogue);
            }
        }
        return;
    }

    private void checkQuizExists()
    {
        if (quizNPC == null)
        {
            quizExists = false;
        }
        else
        {
            quizExists = true;
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
