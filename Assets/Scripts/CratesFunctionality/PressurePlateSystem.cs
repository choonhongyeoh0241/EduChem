using UnityEngine;

public class PressurePlateSystem : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private PressurePlate[] currentPlates;

    private void Awake()
    {
        for (int i = 0; i < currentPlates.Length; i++)
        {
            currentPlates[i].Instance = this;
        }
    }
    public void Check()
    {
        for (int i = 0; i < currentPlates.Length; i++)
        {
            // If any plates are not activated, end the function here.
            if (!currentPlates[i].activated) return;
        }

        AllActivated();
    }

    private void AllActivated()
    {
        boxCollider.isTrigger = true;
    }
}
