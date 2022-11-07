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

        // Debug.Log("Min" + minPosition);
        // Debug.Log("Max" + maxPosition);

        for (float x = box.bounds.min.x; x < minPosition; x++)
        {
            for (float y = box.bounds.min.y; y < maxPosition; y++)
            {
                Instantiate(water, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
