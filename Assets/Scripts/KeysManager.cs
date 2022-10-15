using UnityEngine;
using System.Collections.Generic;

public class KeysManager : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    private Animator animator;
    private void Awake() =>  animator = GetComponent<Animator>();

    private void Update() 
    {
        if (Input.GetKey(key))
        {
            // Debug.Log($"{key} is pressed");
            animator.SetBool("isPressed", true);
        }
        else
        {
            animator.SetBool("isPressed", false);
        }
    }
}
