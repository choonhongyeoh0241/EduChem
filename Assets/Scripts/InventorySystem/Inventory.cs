using System.Collections.Generic; 
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Inventory")]
public class Inventory : ScriptableObject
{
    public List<BookData> books;

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

    private void Load() => SaveManager.Instance.LoadInventoryOverwrite(this);
    public void Add(BookData book)
    {
        if (!books.Contains(book))
        {
            books.Add(book);
            SaveManager.Instance.SetFlag(book);
        }
    }

    public void Remove(BookData book)
    {
        books.Remove(book);
        SaveManager.Instance.ClearFlag(book);
    }

    public bool Contains(BookData book)
    {
        return books.Contains(book);
    }

    public void Clear()
    {
        books.Clear();
    }

}
