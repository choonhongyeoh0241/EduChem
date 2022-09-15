using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static string path => $"{Application.persistentDataPath}/save.json";

    public static Action OnLoad;

    public static SaveManager Instance { get; private set; }

    public (string scene, Vector2 position) location => (saveData.scene, saveData.position);

    [SerializeField] private BookDatabase bookDatabase;
    [SerializeField] private QuizDatabase quizDatabase;
    [SerializeField] private SaveData saveData = new SaveData();

    private void SingletonCheck() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Awake()
    {
        SingletonCheck();
    }

    public void New()
    {
        saveData = new SaveData();
        OnLoad?.Invoke();
    }

    public void Save()
    {
        saveData.scene = SceneManager.GetActiveScene().name;
        saveData.position = FindObjectOfType<PlayerController>().transform.position;
        string json = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(path, json);
    }

    public void Load()
    {
        try
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load save data, using new data instead — {e}");
            New();
        }
        OnLoad?.Invoke();
    }

    public bool GetFlag(SaveData.Flag flag, string dataName)
    {
        var data = FormatData(dataName);
        switch (flag)
        {
            default:
                return false;
            case SaveData.Flag.Quiz:
                return saveData.quizzes.Contains(data);
            case SaveData.Flag.Crate:
                return saveData.crates.Contains(data);
        }
    }

    public bool GetFlag(BookData book)
    {
        return saveData.books.Contains(book.name);
    }

    public bool GetQuizFlag(QuizData quiz)
    {
        return saveData.completeQuizzes.Contains(quiz.name);
    }

    public void SetFlag(SaveData.Flag flag, string dataName)
    {
        var data = FormatData(dataName);
        switch (flag)
        {
            default:
                break;
            case SaveData.Flag.Quiz:
                if (!saveData.quizzes.Contains(data))
                    saveData.quizzes.Add(data);
                break;
            case SaveData.Flag.Crate:
                if (!saveData.crates.Contains(data))
                    saveData.crates.Add(data);
                break;
        }
    }

    public void SetFlag(BookData book)
    {
        if (!saveData.books.Contains(book.name))
            saveData.books.Add(book.name);
    }

    public void SetQuizFlag(QuizData quiz)
    {
        if (!saveData.completeQuizzes.Contains(quiz.name))
            saveData.completeQuizzes.Add(quiz.name);
    }

    public void ClearFlag(BookData book)
    {
        saveData.books.Remove(book.name);
    }

    public void ClearQuizFlag(QuizData quiz)
    {
        saveData.completeQuizzes.Remove(quiz.name);
    }

    public void LoadInventoryOverwrite(Inventory inventory)
    {
        inventory.Clear();

        for (int i = 0; i < saveData.books.Count; i++)
        {
            var book = bookDatabase.GetBookByName(saveData.books[i]);
            if (book != null)
            {
                inventory.Add(book);
            }
            else
            {
                Debug.LogWarning($"{saveData.books[i]} was not found in the database — skipped.");
            }
        }
    }

    public void LoadRestrictOverwrite(QuizScriptable restrictData)
    {
        restrictData.Clear();

        for (int i = 0; i < saveData.completeQuizzes.Count; i++)
        {
            var quiz = quizDatabase.GetQuizByName(saveData.completeQuizzes[i]);
            if (quiz != null)
            {
                restrictData.Add(quiz);
            }
            else
            {
                Debug.LogWarning($"{saveData.completeQuizzes[i]} was not found in the database — skipped.");
            }
        }
    }

    private string FormatData(string data)
    {
        var scene = SceneManager.GetActiveScene().name;
        return $"{scene}; {data}";
    }
}
