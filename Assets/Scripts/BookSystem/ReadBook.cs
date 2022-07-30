using UnityEngine;

public class ReadBook : MonoBehaviour
{
    [SerializeField] protected BookData book; 

    protected PlayerController player;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player != null)
        {
            BookUI.ReadBook(book); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player")) 
        {
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            player = null;
        }
    }
}