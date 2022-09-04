using UnityEngine;
using System.Collections.Generic;

public class QuizRestrict : MonoBehaviour
{
    [SerializeField] private List<QuizData> requiredQuiz;
    [SerializeField] private QuizScriptable quizScriptable;
    [SerializeField] private DialogueData dialogue;
    private bool playerInRange;

    private void Update() 
    {
        checkQuiz();
        
        if (requiredQuiz.Count == 0) 
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

    private void checkQuiz()
    {
        for (int i = 0; i < requiredQuiz.Count; i++)
        {
            if (quizScriptable.Contains(requiredQuiz[i]))
            {
                requiredQuiz.Remove(requiredQuiz[i]);
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
