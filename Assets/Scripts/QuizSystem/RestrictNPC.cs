using UnityEngine;

 #if UNITY_EDITOR
 using UnityEditor;
 #endif
 
 public class RestrictNPC : MonoBehaviour
 {
    [HideInInspector] 
    [SerializeField] private bool _isQuiz;
    public bool isQuiz {get => _isQuiz; set{_isQuiz = value;}}

    [HideInInspector] 
    [SerializeField] private bool _isBook;
    public bool isBook {get => _isBook; set{_isBook = value;}}
 
    [HideInInspector]
    [SerializeField] private int _requiredBookCount;
    public int requiredBookCount {get => _requiredBookCount; set{_requiredBookCount = value;}}

    [HideInInspector]
    [SerializeField] private int _requiredQuizCount;
    public int requiredQuizCount {get => _requiredQuizCount; set{_requiredQuizCount = value;}}

    [HideInInspector]
    [SerializeField] private RestrictData _restrictData;
    public RestrictData restrictData {get => _restrictData; set{_restrictData = value;}}
    [HideInInspector]
    [SerializeField] private Inventory _inventory;
    public Inventory inventory {get => _inventory; set{_inventory = value;}}

    [HideInInspector]
    private bool playerInRange;

    [HideInInspector]
    [SerializeField] private DialogueData _quizDialogue, _bookDialogue;
    public DialogueData quizDialogue {get => _quizDialogue; set{ _quizDialogue = value;}}
    public DialogueData bookDialogue {get => _bookDialogue; set{ _bookDialogue = value;}} 

    private void Update() 
    {
        if (isQuiz)
        {
            if (restrictData.completedQuiz.Count >= requiredQuizCount)
                {
                    gameObject.SetActive(false);
                }
            else
            {
                if (playerInRange && Input.GetKeyDown(KeyCode.Space))
                {
                    DialogueManager.RequestDialogue(quizDialogue);
                }
                return;
            }
        }
        else if (isBook)
        {
            if (inventory.books.Count >= requiredBookCount)
                {
                    gameObject.SetActive(false);
                }
            else
            {
                if (playerInRange && Input.GetKeyDown(KeyCode.Space))
                {
                    DialogueManager.RequestDialogue(bookDialogue);
                }
                return;
            }
            return;
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
 
 #if UNITY_EDITOR
 [CustomEditor(typeof(RestrictNPC))]
 public class RestrictNPC_Editor : Editor
 {
     public override void OnInspectorGUI()
     {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        RestrictNPC script = (RestrictNPC)target;
 
         // draw checkbox for the bool
        script.isQuiz = EditorGUILayout.Toggle("isQuiz", script.isQuiz);
        script.isBook = EditorGUILayout.Toggle("isBook", script.isBook);
        if (script.isQuiz) // if bool is true, show other fields
        {
            script.requiredQuizCount = EditorGUILayout.IntField("Required Quiz Count:" , script.requiredQuizCount);
            script.restrictData = EditorGUILayout.ObjectField("Restrict Data", script.restrictData, typeof(RestrictData), true) as RestrictData;
            script.quizDialogue = EditorGUILayout.ObjectField("Dialogue Data", script.quizDialogue, typeof(DialogueData), true) as DialogueData;
        }
        else if (script.isBook)
        {
            script.requiredBookCount = EditorGUILayout.IntField("Required Book Count:" , script.requiredBookCount);
            script.inventory = EditorGUILayout.ObjectField("Inventory", script.inventory, typeof(Inventory), true) as Inventory;
            script.bookDialogue = EditorGUILayout.ObjectField("Dialogue Data", script.bookDialogue, typeof(DialogueData), true) as DialogueData;
        }

        EditorUtility.SetDirty(script);
     }
 }
 #endif