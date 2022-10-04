using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Crate : MonoBehaviour
{
    [SerializeField] [Multiline] private string sign;
    private bool playerInRange;

    private void Update() 
    {
        if (playerInRange)
        {
            CrateManager.RaiseSign(sign);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            playerInRange = false;
            CrateManager.RaiseSign(null);
        }
    }
}
