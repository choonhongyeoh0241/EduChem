using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MultipleChoiceQuestion.Response))]
public class QuizDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        var space = 5;
        var isAnswerWidth = 20;
        var responseWidth = position.width - isAnswerWidth;
        var responseRect = new Rect(position.x, position.y, responseWidth, position.height);
        var isAnswerRect = new Rect(position.x + responseWidth + space, position.y, isAnswerWidth, position.height);

        EditorGUI.PropertyField(responseRect, property.FindPropertyRelative("option"), GUIContent.none);
        EditorGUI.PropertyField(isAnswerRect, property.FindPropertyRelative("isAnswer"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}