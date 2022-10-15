using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pause;

public class InventoryUI : MonoBehaviour, IPauser
{
    [Header("Components")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI bookTitle;
    [SerializeField] private Button readButton;
    [SerializeField] private GameObject slotsContainer;

    private int offset;
    private Inventory inventory;
    private InventorySlot[] slots;
    private BookData bookSelected;
    private static Action<Inventory> OnInventoryRequested;
    public static void RequestInventory(Inventory inventory) => OnInventoryRequested?.Invoke(inventory); 

    public bool active => inventoryPanel.activeSelf;
    private void OnEnable() => OnInventoryRequested += OpenInventory;
    private void OnDisable() => OnInventoryRequested -= OpenInventory;
    
    private void OpenInventory(Inventory inventory)
    {
        if (inventory == null) return;

        this.inventory = inventory;

        if (slots == null) GetSlots(); 

        UpdateInventory();

        inventoryPanel.SetActive(true);
    }

    private void UpdateInventory()
    {
        PopulateSlots();
        UpdateCounter();
        UpdateButtons();
        UpdateBookTitle();
        UpdateReadButton();
    }

    private void GetSlots()
    {
        slots = slotsContainer.GetComponentsInChildren<InventorySlot>(true);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].manager = this;
        }
    }

    private void PopulateSlots()
    {
        for (int i = 0, j = offset * slots.Length; i < slots.Length; i++, j++)
        {
            if (j < inventory.books.Count)
            {
                slots[i].book = inventory.books[j];
            }
            else
            {
                slots[i].book = null;
            }
        }
    }

    private void UpdateCounter()
    {
        if (inventory.books.Count > 0)
        {
            int currentPage = offset + 1;
            int totalPages = Mathf.CeilToInt((float)inventory.books.Count / slots.Length);
            counter.text = $"{currentPage}/{totalPages}";
        }
        else
        {
            counter.text = $"{0}/{0}";
        }
        
    }

    private void UpdateButtons()
    {
        int currentPage = offset + 1;
        int totalPages = Mathf.CeilToInt((float)inventory.books.Count / slots.Length);
        leftButton.gameObject.SetActive(currentPage > 1);
        rightButton.gameObject.SetActive(currentPage < totalPages);
    }

    private void UpdateBookTitle()
    {
        bookTitle.text = bookSelected ? bookSelected.title : "NO BOOK SELECTED";
    }

    private void UpdateReadButton()
    {
        readButton.gameObject.SetActive(bookSelected != null);
    }

    public void ReadBook()
    {
        // Debug.Log($"Now Reading{bookSelected.name}");
        BookUI.ReadBook(bookSelected);
        Close();
    }

    public void TurnPage(Button sideButton)
    {
        if (sideButton == rightButton)
        {
            // Debug.Log("Next page of inventory");
            offset ++;
        }
        else
        {
            // Debug.Log("Previous page of inventory");
            offset --;
        }

        UpdateInventory();
    }

    public void Select(BookData book)
    {
        bookSelected = book;
        UpdateBookTitle();
        UpdateReadButton();
    }

    public void Close()
    {
        // Debug.Log("Close Inventory");
        inventory = null;
        bookSelected = null;
        offset = 0;
        inventoryPanel.SetActive(false);
    }
}
