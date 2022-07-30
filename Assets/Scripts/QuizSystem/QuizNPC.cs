using UnityEngine;

public class QuizNPC : MonoBehaviour
{
    [SerializeField] private QuizData quiz;
    private bool playerInRange;

    private void Start()
    {
        if (quiz != null && SaveManager.Instance.GetFlag(SaveData.Flag.Quiz, quiz.name))
        {
            Destroy(this);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            QuizManager.RequestQuiz(quiz, this);
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
            QuizManager.RequestQuiz(null);
        }
    }
}
