using UnityEngine;
using UnityEngine.UI;
using System;
using Pause;

public class PauseMenu : MonoBehaviour, IPauser
{
    private static Action OnPauseMenuRequested;
    public static void RequestMenu() => OnPauseMenuRequested?.Invoke();

    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image volumeSetting;

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

    public void Volume()
    {
        pauseMenu.SetActive(false);
        volumeSetting.gameObject.SetActive(true);
    }

    public void Save()
    {
        SaveManager.Instance.Save();
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
}
