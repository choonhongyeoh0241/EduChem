using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    public void PlaySFX()
    {
        AudioManager.Instance.PlaySound(_clip);
        // Debug.Log($"{_clip.name} played");
    }
}
