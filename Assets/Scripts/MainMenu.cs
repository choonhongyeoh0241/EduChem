using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject overwriteWarning;
    [SerializeField] private Button loadButton;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject MenuCanvas;

    private bool saveExists => System.IO.File.Exists(SaveManager.path);
    

    private void Awake()
    {
        if (!saveExists)
        {
            loadButton.interactable = false;
        }
    }

    public void PlayGame()
    {
        if (saveExists)
        {
            overwriteWarning.SetActive(true);
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        SaveManager.Instance.New();
        PlayerPrefs.DeleteKey("timeline");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SaveManager.Instance.Load();

        if (string.IsNullOrWhiteSpace(SaveManager.Instance.location.scene))
        {
            NewGame();
        }
        else
        {
            SceneManager.LoadScene(SaveManager.Instance.location.scene);
            SceneAnchor.transitionPosition = SaveManager.Instance.location.position;
        }
    }

    public void ViewScore()
    {
        MenuCanvas.SetActive(false);
        scorePanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
