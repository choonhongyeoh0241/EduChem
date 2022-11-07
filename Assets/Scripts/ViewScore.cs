using UnityEngine;
using LitJson;
using TMPro;
using System.IO;

public class ViewScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScene;
    [SerializeField] private TextMeshProUGUI currentBooksCount;
    [SerializeField] private TextMeshProUGUI currentQuizCount;
    [SerializeField] private TextMeshProUGUI currentCratesCount;

    [SerializeField] private static string _sceneName;

    [SerializeField] private static int _collectedBooks;
    [SerializeField] private static int _completedCrates;
    [SerializeField] private static int _completeCounts;

    private string jsonString;
    private JsonData itemData;
    
    private void Start() 
    {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/save.json");
        itemData = JsonMapper.ToObject(jsonString);

        GetScene(itemData);
        GetQuizzes(itemData);
        GetBooks(itemData);
        GetCrates(itemData);

        UpdateCounts();
    }

    private void UpdateCounts()
    {
        currentScene.text = _sceneName;
        currentBooksCount.text = _collectedBooks.ToString();
        currentQuizCount.text = _completeCounts.ToString();
        currentCratesCount.text = _completedCrates.ToString();
    }

    JsonData GetScene(JsonData data)
    {
        _sceneName = data[0].ToString();
        return null;
    }

    JsonData GetBooks (JsonData data)
    {
        _collectedBooks = data[3].Count;
        return null;
    }

    JsonData GetCrates (JsonData data)
    {
        _completedCrates = data[4].Count;
        return null;
    }

    JsonData GetQuizzes (JsonData data)
    {
        _completeCounts = data[5].Count;
        return null;
    }
}
