using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image NetworkFail;
    [SerializeField] private Image NetworkSuccess;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Image Checking;
    [SerializeField] private GameObject overwriteWarning;
    [SerializeField] private Button loadButton;

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

    public void Upload()
    {
        mainCanvas.enabled = false;
        Checking.gameObject.SetActive(true);
        StartCoroutine(CheckingScreen());
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public IEnumerator CheckConnection()
    {
        UnityWebRequest request = new UnityWebRequest("https://google.com");

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            NetworkFail.gameObject.SetActive(true);
            NetworkSuccess.gameObject.SetActive(false);
            Checking.gameObject.SetActive(false);
        }
        else
        {
            NetworkSuccess.gameObject.SetActive(true);
            NetworkFail.gameObject.SetActive(false);
            Checking.gameObject.SetActive(false);
        }
    }

    public void TryAgain()
    {
        StartCoroutine(CheckConnection());
    }

    private IEnumerator CheckingScreen()
    {
        yield return new WaitForSeconds(3);
    
        StartCoroutine(CheckConnection());
    }
}
