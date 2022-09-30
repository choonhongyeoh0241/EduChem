using UnityEngine;

public class PressurePlateSystem : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private PressurePlate[] plates;

    private void Awake()
    {
        for (int i = 0; i < plates.Length; i++)
        {
            plates[i].manager = this;
        }
    }

    public void Check()
    {
        for (int i = 0; i < plates.Length; i++)
        {
            // If any plates are not activated, end the function here.
            if (!plates[i].activated) return;
        }

        AllActivated();
    }

    private void AllActivated()
    {
        block.SetActive(false);
    }
}
