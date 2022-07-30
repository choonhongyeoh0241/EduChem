using UnityEngine;

public class PickUp : ReadBook
{
    private void OnValidate()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null && book != null)
        {
            sprite.color = book.color;
        }
    }

    [SerializeField] private bool pickUp = true;

    private void Start()
    {
        if (SaveManager.Instance.GetFlag(book))
        {
            gameObject.SetActive(false);
        }
    }

    protected override void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && player != null)
        {
            Pickup();
        }
        
    }
    private void Pickup()
    {
        if (pickUp) BookUI.ReadBook(book);
        player.inventory.Add(book);
        gameObject.SetActive(false);
    }
}
