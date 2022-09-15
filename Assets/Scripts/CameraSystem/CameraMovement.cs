using System;
using UnityEngine;

[RequireComponent(typeof(Camera))] 
public class CameraMovement : MonoBehaviour
{
    public static void RaiseCameraBoundsChange(Bounds bounds) => OnCameraBoundsChange?.Invoke(bounds);
    private static Action<Bounds> OnCameraBoundsChange;

    [SerializeField] private Transform target = default;
    [SerializeField] private float smoothing = 5;

    private Vector2 minPosition;
    private Vector2 maxPosition;

    private new Camera camera;

    private void Awake() => OnCameraBoundsChange += UpdateBounds;
    private void OnDestroy() => OnCameraBoundsChange -= UpdateBounds;

    private void LateUpdate()
    {
        if (transform.position != target.position) 
        { 
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x); 
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); 

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing); 
        }
    }

    private void UpdateBounds(Bounds bounds) 
    {
        if (camera == null) camera = GetComponent<Camera>(); 

        var halfSize = new Vector2(camera.orthographicSize * camera.aspect, camera.orthographicSize); 
        minPosition = (Vector2)bounds.min + halfSize; 
        maxPosition = (Vector2)bounds.max - halfSize; 
    }
}