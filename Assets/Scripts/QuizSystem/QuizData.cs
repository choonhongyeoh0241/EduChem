using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Quiz Data")]
public class QuizData : ScriptableObject
{
    [SerializeField] private MultipleChoiceQuestion[] _MCQ;
    public MultipleChoiceQuestion[] MCQ => _MCQ;
}

[System.Serializable]
public class MultipleChoiceQuestion 
{
    [TextArea] public string question;
    public Response[] options;

    [System.Serializable]
    public class Response
    {
        public string option;
        public bool isAnswer;
    }
}
