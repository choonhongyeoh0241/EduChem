// using UnityEngine;

//  #if UNITY_EDITOR
//  using UnityEditor;
//  #endif
 
//  [System.Serializable]
//  public class RestrictNPC : MonoBehaviour
//  {
//     [HideInInspector] 
//     [SerializeField] private bool _isQuiz;
//     public bool isQuiz {get => _isQuiz; set{_isQuiz = value;}}

//     [HideInInspector] 
//     [SerializeField] private bool _isBook;
//     public bool isBook {get => _isBook; set{_isBook = value;}}
 
//     [HideInInspector]
//     [SerializeField] private BookData[] _requiredBooks;
//     public BookData[] requiredBook {get => _requiredBooks; set{_requiredBooks = value;}}

//     [HideInInspector]
//     [SerializeField] private QuizData[] _requiredQuiz;
//     public QuizData[] requiredQuiz {get => _requiredQuiz; set{_requiredQuiz = value;}}

//     [HideInInspector]
//     [SerializeField] private RestrictData _restrictData;
//     public RestrictData restrictData {get => _restrictData; set{_restrictData = value;}}
//     [HideInInspector]
//     [SerializeField] private Inventory _inventory;
//     public Inventory inventory {get => _inventory; set{_inventory = value;}}

//     [HideInInspector]
//     private bool playerInRange;

//     [HideInInspector]
//     [SerializeField] private DialogueData _quizDialogue, _bookDialogue;
//     public DialogueData quizDialogue {get => _quizDialogue; set{ _quizDialogue = value;}}
//     public DialogueData bookDialogue {get => _bookDialogue; set{ _bookDialogue = value;}} 

//     private void Update() 
//     {
//         if (isQuiz)
//         {
//             for (int i = 0; i < restrictData.completedQuiz.Count; i++)
//             {
//                 for (int j = 0; j < requiredQuiz.Length; j++)
//                 {
//                     if(restrictData.completedQuiz[i] == requiredQuiz[j])
//                     {
//                         gameObject.SetActive(false);
//                     }
//                     else
//                     {
//                         if (playerInRange && Input.GetKeyDown(KeyCode.Space))
//                         {
//                             DialogueManager.RequestDialogue(quizDialogue);
//                         }
//                         return;
//                     }
//                 }
//             }
//         }
//         else if (isBook)
//         {
//             for (int i = 0; i < inventory.books.Count; i++)
//             {
//                 for (int j = 0; j < requiredBook.Length; j++)
//                 {
//                     if(inventory.books[i] == requiredBook[j])
//                     {
//                         gameObject.SetActive(false);
//                     }
//                     else
//                     {
//                         if (playerInRange && Input.GetKeyDown(KeyCode.Space))
//                         {
//                             DialogueManager.RequestDialogue(bookDialogue);
//                         }
//                         return;
//                     }
//                 }
//             }
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (!other.isTrigger && other.CompareTag("Player"))
//         {
//             playerInRange = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (!other.isTrigger && other.CompareTag("Player"))
//         {
//             playerInRange = false;
//             QuizManager.RequestQuiz(null);
//         }
//     }
// }
 
//  #if UNITY_EDITOR
//  [CustomEditor(typeof(RestrictNPC))]
//  public class RestrictNPC_Editor : Editor
//  {
//     private SerializedObject sObj;
//     private RestrictNPC rNPC;
//     private SerializedProperty requiredQuiz;

//     private void OnEnable()
//     {
//         sObj = new SerializedObject(target);
//         rNPC = (RestrictNPC) target;
//         requiredQuiz = serializedObject.FindProperty("requiredQuiz");
//     }

//      public override void OnInspectorGUI()
//      {
//         sObj.Update();

//         DrawDefaultInspector(); // for other non-HideInInspector fields

//         RestrictNPC script = (RestrictNPC)target;
 
//          // draw checkbox for the bool
//         script.isQuiz = EditorGUILayout.Toggle("isQuiz", script.isQuiz);
//         script.isBook = EditorGUILayout.Toggle("isBook", script.isBook);
//         if (script.isQuiz) // if bool is true, show other fields
//         {
//             EditorGUILayout.PropertyField(requiredQuiz, new GUIContent("Required Quiz:"), true);
//             script.restrictData = EditorGUILayout.ObjectField("Restrict Data", script.restrictData, typeof(RestrictData), true) as RestrictData;
//             script.quizDialogue = EditorGUILayout.ObjectField("Dialogue Data", script.quizDialogue, typeof(DialogueData), true) as DialogueData;
//         }
//         else if (script.isBook)
//         {
            
//             script.inventory = EditorGUILayout.ObjectField("Inventory", script.inventory, typeof(Inventory), true) as Inventory;
//             script.bookDialogue = EditorGUILayout.ObjectField("Dialogue Data", script.bookDialogue, typeof(DialogueData), true) as DialogueData;
//         }

//         serializedObject.ApplyModifiedProperties();
//         EditorUtility.SetDirty(script);
//      }
//  }
//  #endif