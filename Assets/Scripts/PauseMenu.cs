using UnityEngine;
using TMPro;
using System;
using System.Collections;
using Pause;

public class PauseMenu : MonoBehaviour, IPauser
{
    private static Action OnPauseMenuRequested;
    public static void RequestMenu() => OnPauseMenuRequested?.Invoke();

    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject waitingScreen;
    [SerializeField] private TextMeshProUGUI savingText;
    [SerializeField] private TextMeshProUGUI savedText;

    private void OnEnable() => OnPauseMenuRequested += OpenMenu;
    private void OnDisable() => OnPauseMenuRequested -= OpenMenu;
    private int activeFrame;
    private bool notActivated => activeFrame != Time.frameCount;
    public bool active => pauseMenu.activeSelf;

    private void OpenMenu()
    {
        pauseMenu.SetActive(true);
        activeFrame = Time.frameCount;
    }

    private void CloseMenu()
    {
        pauseMenu.SetActive(false);
    }

    private void LateUpdate() 
    {
        if (active && notActivated && Input.GetKeyDown(KeyCode.Escape)) CloseMenu();
    }
    public void Inventory()
    {
        CloseMenu(); // To close pause menu first then request for Inventory
        InventoryUI.RequestInventory(inventory);
    }

    public void Save()
    {
        SaveManager.Instance.Save();
        StartCoroutine(SavingScreen());

    }

    public void Back()
    {
        CloseMenu();
    }

    public void Exit()
    {
        // Hard-coded, following logic from `MainMenu.cs`
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private IEnumerator SavingScreen()
    {
        waitingScreen.SetActive(true);
        StartCoroutine(saving());
        yield return new WaitForSeconds(6);
        waitingScreen.SetActive(false);
    }

    private IEnumerator saving()
    {
        savingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        savingText.gameObject.SetActive(false);
        StartCoroutine(saved());
    }

    private IEnumerator saved()
    {
        savedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        savedText.gameObject.SetActive(false);
    }
}
