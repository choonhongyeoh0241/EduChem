using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private BoxCollider2D box;

    private void Awake() 
    {
        var minPosition = box.bounds.size.x;
        var maxPosition = box.bounds.size.y;

        for (int x = 0; x < minPosition; x++)
        {
            for (int y = 0; y < maxPosition; y++)
            {
                Instantiate(water, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
