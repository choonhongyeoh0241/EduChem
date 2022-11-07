using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
    public InventoryUI manager { get; set;}
    private BookData _book;

    public BookData book
    {
        get => _book;

        set
        {
            if (_book != value)
            {
                _book = value;
                if (_book != null)
                {
                    image.color = _book.color;
                    gameObject.SetActive(true);
                }
                else
                {
                    image.color = Color.white;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private Image _image;

    public Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            return _image;
        }
    }
    
    public void OnSelect(BaseEventData eventData) 
    {
        manager?.Select(book); 
        // Debug.Log("Book Clicked");
    }
}
