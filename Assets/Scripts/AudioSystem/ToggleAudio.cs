using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleEffect;

    public void Toggle()
    {
        if (_toggleEffect) AudioManager.Instance.ToggleEffect();
        if (_toggleMusic) AudioManager.Instance.ToggleMusic();
    }
}
