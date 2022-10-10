using System;
using UnityEngine;

public class SceneAnchor : MonoBehaviour
{
    public static Action<Vector2> OnSceneTransition;  // Create Action to take in Vector2 type and event for methods to subscribe
    // event is convenient use as there is no direct reference between two classes which might cause conflicts
    private static Vector2? _transitionPosition; // All scene anchor has same value for Vector2, with ? allows it to be null as of out of runtime
    public static Vector2 transitionPosition {
        set {
            _transitionPosition = value; // Setter to set the value of _transitionPosition using value keyword, *value just a keyword, which implicit a parameter
            // Instead of setting it using transitionPosition(Vector2 coordinates), use value keyword will do 
        }
    }

    private void Start()
    {
        if (_transitionPosition.HasValue) { // If vector2 not null, means that player need repositioned
        Debug.Log(_transitionPosition.Value);
            OnSceneTransition?.Invoke(_transitionPosition.Value); // If event has subscription will Invoke Vector2 value
            _transitionPosition = null; // Set Vector2 as null
        }
    }
}
