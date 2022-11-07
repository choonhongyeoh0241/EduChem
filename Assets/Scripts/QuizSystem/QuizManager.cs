using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Random = UnityEngine.Random;
using Pause;

public class QuizManager : MonoBehaviour, IPauser
{
   private static Action<QuizData, QuizNPC> OnQuizRequested;
   public static void RequestQuiz(QuizData quiz, QuizNPC npc = null) => OnQuizRequested?.Invoke(quiz, npc);

   public bool active => quizPanel.activeSelf;

   [Header("Components")]

   [SerializeField] private GameObject quizPanel;
   [SerializeField] private GameObject resultPanel;
   [SerializeField] private TextMeshProUGUI questionText;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private TextMeshProUGUI questionCounter;
   [SerializeField] private QuizOption[] options;
   [SerializeField] private PlayerController player;
   [SerializeField] private Inventory inventory;
   [SerializeField] private Button detect;

   [Header("Feedback")]
   [SerializeField] private TextMeshProUGUI feedbackText;
   [SerializeField][TextArea] private string pass;
   [SerializeField][TextArea] private string fail;

    private Coroutine activeCoroutine;
    private QuizData currentQuiz; 
    private QuizNPC actor; 
    private List<int> remainingQuestions; 
    private int score;
    private int questionIndex;
    private bool passed;

    private void OnEnable() => OnQuizRequested += PrepareQuiz; //Subscribe to the event 
    private void OnDisable() => OnQuizRequested -= PrepareQuiz; // Unsubscribe from the event

    private void PrepareQuiz(QuizData quiz, QuizNPC npc)
    {
        if (quiz != null && currentQuiz != quiz)
        {
            currentQuiz = quiz;
            actor = npc;
            Initialise(quiz);
            GenerateQuestion();
            quizPanel.SetActive(true);
            detect.gameObject.SetActive(true);
        }
        else
        {
            quizPanel.SetActive(false);
            ClearData();
        }
    }

    private void Initialise(QuizData quiz)
    {
        remainingQuestions = new List<int>(); 
        for (int i = 0; i < quiz.MCQ.Length; i++)
        {                                              
            remainingQuestions.Add(i);     
        }
    }

    // ClearData implemented as name suggests to clear data if quiz is not all correctly answered so that player can redo
    private void ClearData()
    {
        currentQuiz = null;
        actor = null;
        remainingQuestions?.Clear();
        score = 0;
        questionIndex = 0;
        passed = false;
    }

    // ShowResults implemented so that each turn of doing quiz let player knows if they can move on or not
    private void ShowResults()
    {
        quizPanel.SetActive(false);
        detect.gameObject.SetActive(false);
        resultPanel.SetActive(true);
        scoreText.text = score + "/" + currentQuiz.MCQ.Length;
        passed = score >= currentQuiz.MCQ.Length;
        if (passed) 
        {
            SaveManager.Instance.SetFlag(SaveData.Flag.Quiz, currentQuiz.name);
            SaveManager.Instance.SetQuizFlag(currentQuiz);
            player.restrict.Add(currentQuiz);
            feedbackText.text = pass;
        }
        else
        {
            feedbackText.text = fail;
        }
    }

    // IEnumerator, an interface to use WaitForSeconds function
    private IEnumerator NextQuestionDelay()
    {
        // For every 1 second after a question done, this function will check if question and responses left
        // If instances of QuizData is not null, will call GenerateQuestion function 
        // When such happens, the quiz state won't be delaying
        yield return new WaitForSeconds(1);
        if (currentQuiz != null) GenerateQuestion();
        activeCoroutine = null; 
    }

    private void GenerateQuestion()
    {
        if (remainingQuestions.Count > 0)
        {
            int i = remainingQuestions[Random.Range(0, remainingQuestions.Count)];
            questionIndex ++;
            questionCounter.text = questionIndex + "/" + currentQuiz.MCQ.Length;
            questionText.text = currentQuiz.MCQ[i].question;
            SetResponses(currentQuiz.MCQ[i]);
            // Debug.Log("Question Generated");
            remainingQuestions.Remove(i);
        }
        else
        {
            ShowResults();
        }
    }

    private void SetResponses(MultipleChoiceQuestion question)
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].response = question.options[i];
        }
    }

    public void Check(QuizOption option)
    {
        if (activeCoroutine != null) return;

        if (option.response.isAnswer) score++;
        option.AnswerColor(option.response.isAnswer);
        // Debug.Log("Checked Answer");
        activeCoroutine = StartCoroutine(NextQuestionDelay());
    }

    public void OpenInventory()
    {
        InventoryUI.RequestInventory(inventory);
    }

    public void Exit()
    {
        // if (passed) actor?.gameObject.SetActive(false);
        if (passed)
        {
            if (actor != null)
            {
                Destroy(actor.GetComponent<QuizNPC>());
            }
        }
        resultPanel.SetActive(false); 
        // Debug.Log("Exit button clicked, score panel deactivated");
        ClearData(); 
    }

    public void Detect()
    {
        if (active) quizPanel.SetActive(false); detect.gameObject.SetActive(false);
    }
}
