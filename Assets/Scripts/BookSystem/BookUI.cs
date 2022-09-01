using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pause;

public class BookUI : MonoBehaviour, IPauser
{
    private static event Action<BookData> OnBookRead;
    public static void ReadBook(BookData book) => OnBookRead?.Invoke(book);
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject closeButton;

    private Canvas bookCanvas;
    private BookData currentBook;
    public bool active => bookCanvas.enabled;

    private int index;

    private void Awake() => bookCanvas = GetComponent<Canvas>();

    private void OnEnable() => OnBookRead += StartBook;
    private void OnDisable() => OnBookRead -= StartBook;

    private void StartBook(BookData book)
    {
        if (book == currentBook) return;

        ActiveBook(true);
        index = 0;
        currentBook = book;
        Render();
    }

    private void CloseBook()
    {
        ActiveBook(false);
        currentBook = null;
        index = 0;
    }

    private void ActiveBook(bool isTrue)
    {
        bookCanvas.enabled = isTrue;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isTrue);
        }
    }

    private void Render()
    {
        RenderPage(index);
        RenderPage(index + 1);
    }

    private void RenderPage(int page)
    {
        var isEven = (page % 2 == 0);
        var text = isEven ? leftText : rightText;
        if (page < currentBook.pages.Length)
        {
            text.text = currentBook.pages[page];
            if(isEven)
            {
                leftButton.SetActive(page > 0);
            }
            else
            {
                if (currentBook.pages.Length <= page + 1) 
                {
                    closeButton.SetActive(true);
                    rightButton.SetActive(false);
                }
                else
                {
                    rightButton.SetActive(true);
                    closeButton.SetActive(false);
                }
            }
        }
        else
        {
            text.text = "";
            if (!isEven) 
            {
                closeButton.SetActive(true);
                rightButton.SetActive(false);
            }
        }
    }

    public void TurnPage(GameObject sideButton)
    {
        if(sideButton == rightButton) index += 2;
        if(sideButton == leftButton) index -=2;
        if(sideButton == closeButton) 
        {
            CloseBook(); 
            return;
        }

        Render();
    }

}
