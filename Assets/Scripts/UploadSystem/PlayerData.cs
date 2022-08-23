using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Proyecto26;
using TMPro;
using LitJson;
using System.IO;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameText; 
    public TMP_InputField nameText => _nameText; 
    [SerializeField] private TMP_InputField _schoolText; 
    public TMP_InputField schoolText => _schoolText;    
    [SerializeField] private TextMeshProUGUI currentScene;
    [SerializeField] private TextMeshProUGUI currentBooksCount;
    [SerializeField] private TextMeshProUGUI currentQuizCount;
    [SerializeField] private TextMeshProUGUI currentCratesCount;
    [SerializeField] private Image BackToMain;
    [SerializeField] private Image WaitLoading;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Image SuccessBG;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button clearButton;

    [SerializeField] private static string _schoolName;
    public static string schoolName {get => _schoolName; set{_schoolName = value;}}
    [SerializeField] private static string _playerName;
    public static string playerName {get => _playerName; set{_playerName = value;}}
    [SerializeField] private static string _sceneName;
    public static string sceneName {get =>_sceneName; set {_sceneName = value;}}
    [SerializeField] private static int _collectedBooks;
    public static int collectedBooks {get =>_collectedBooks; set {_collectedBooks = value;}}
    [SerializeField] private static int _completedCrates;
    public static int completedCrates {get =>_completedCrates; set {_completedCrates = value;}}
    [SerializeField] private static int _completeCounts;
    public static int completeCounts {get =>_completeCounts; set {_completeCounts = value;}}

    private bool activeLoad => BackToMain.isActiveAndEnabled;
    private bool activeSubmit => WaitLoading.isActiveAndEnabled;
    private User user;
    private string jsonString;
    private JsonData itemData;
    private static readonly string databaseURL = "https://educhem-b13a6-default-rtdb.asia-southeast1.firebasedatabase.app/";
    private static readonly string reference = $"{databaseURL}{schoolName}";
    private void Start() 
    {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/save.json");
        itemData = JsonMapper.ToObject(jsonString);

        playerName = PlayerPrefs.GetString("userName");
        schoolName = PlayerPrefs.GetString("school");

        if (nameText.text != "" && schoolText.text != "")
        {
            nameText.text = playerName;
            schoolText.text = schoolName;
            nameText.interactable = false;
            schoolText.interactable = false;
        }
        else
        {
            nameText.interactable = true;
            schoolText.interactable = true;
        }

        GetScene(itemData);
        GetQuizzes(itemData);
        GetBooks(itemData);
        GetCrates(itemData);

        UpdateCounts();
    }

    private void Update() 
    {
        string player = nameText.text;
        string school = schoolText.text;

        if (activeLoad || activeSubmit)
        {
            submitButton.interactable = false;
        }
        else
        {
            clearButton.interactable = true;
            CheckInput(player, school);
        } 
    }

    private void UpdateCounts()
    {
        currentScene.text = sceneName;
        currentBooksCount.text = collectedBooks.ToString();
        currentQuizCount.text = completeCounts.ToString();
        currentCratesCount.text = completedCrates.ToString();
    }

    private void CheckInput(string playerNameT, string schoolNameT)
    {
        if (string.IsNullOrEmpty(playerNameT) || string.IsNullOrEmpty(schoolNameT))
        {
            submitButton.interactable = false;
        }
        else
        {
            submitButton.interactable = true;
        }
    }

    JsonData GetScene(JsonData data)
    {
        sceneName = data[0].ToString();
        return null;
    }

    JsonData GetBooks (JsonData data)
    {
        collectedBooks = data[3].Count;
        return null;
    }

    JsonData GetCrates (JsonData data)
    {
        completedCrates = data[4].Count;
        return null;
    }

    JsonData GetQuizzes (JsonData data)
    {
        completeCounts = data[5].Count;
        return null;
    }

    public void OnSubmit()
    {
        
        playerName = nameText.text;
        schoolName = schoolText.text;

        PlayerPrefs.SetString("userName", playerName);
        PlayerPrefs.SetString("school", schoolName); 

        nameText.interactable = false;
        schoolText.interactable = false;

        PostToDatabase();
        StartCoroutine(LoadBack());
    }

    public void OnRename()
    {
        PlayerPrefs.DeleteAll();
        RestClient.Delete($"{databaseURL}/{playerName}.json");
        nameText.text = string.Empty;
        schoolText.text = string.Empty;
        nameText.interactable = true;
        schoolText.interactable = true;
        // RestClient.Get<UserMap>($"{databaseURL}").Then(res =>
        // {
        //     schoolName = PlayerPrefs.GetString("school");
        //     foreach(string schoolName in res.userData.Keys)
        //     {
        //         Debug.Log("Key");
        //         Debug.Log(schoolName);
        //     }
        // });

    }

    private IEnumerator LoadBack()
    {
        clearButton.interactable = false;
        WaitLoading.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        WaitLoading.gameObject.SetActive(false);
        BackToMain.gameObject.SetActive(true);
    }

    public void Back()
    {
        BackToMain.gameObject.SetActive(false);
        SuccessBG.gameObject.SetActive(false);
        mainCanvas.enabled = true;
    }

    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put<User>($"{databaseURL}{schoolName}/{playerName}.json", user);
    }
    
}
