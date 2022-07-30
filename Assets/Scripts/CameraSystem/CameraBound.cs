using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraBound : MonoBehaviour 
{
    [SerializeField] private BoxCollider2D boxCollider;

    private void Awake() => boxCollider = GetComponent<BoxCollider2D>(); 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.CompareTag("Player"))
        {
            CameraMovement.RaiseCameraBoundsChange(boxCollider.bounds); 
        }
    }
}