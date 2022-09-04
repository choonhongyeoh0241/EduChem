using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Restirct Data")]
public class QuizScriptable : ScriptableObject
{
    public List<QuizData> completedQuiz;

    private void OnEnable()
    {
        #if UNITY_EDITOR
        // https://forum.unity.com/threads/reliable-way-to-detect-when-the-game-starts-playing-on-a-scriptableobject.945353/
        // Only overwrite during runtime
        UnityEditor.EditorApplication.playModeStateChanged += EditorPlayModeOnEnable;
        if (!UnityEditor.EditorApplication.isPlaying) return;
        #endif

        Load();
        SaveManager.OnLoad += Load;
    }

    private void OnDisable()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged -= EditorPlayModeOnEnable;
        #endif

        SaveManager.OnLoad -= Load;
    }

    #if UNITY_EDITOR
    private void EditorPlayModeOnEnable(UnityEditor.PlayModeStateChange state) {
        if (state == UnityEditor.PlayModeStateChange.EnteredPlayMode) OnEnable();
    }
    #endif

    private void Load() => SaveManager.Instance.LoadRestrictOverwrite(this);
    public void Add(QuizData quiz)
    {
        if (!completedQuiz.Contains(quiz))
        {
            completedQuiz.Add(quiz);
            SaveManager.Instance.SetQuizFlag(quiz);
        }
    }

    public void Remove(QuizData quiz)
    {
        completedQuiz.Remove(quiz);
        SaveManager.Instance.ClearQuizFlag(quiz);
    }

    public bool Contains(QuizData quiz)
    {
        return completedQuiz.Contains(quiz);
    }

    public void Clear()
    {
        completedQuiz.Clear();
    }
}
