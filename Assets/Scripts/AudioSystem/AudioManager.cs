using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [SerializeField] private AudioSource _music, _soundEffect;
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() 
    {
        _music.loop = true;
        // Debug.Log($"Music {_music.name} is now looping");

        var value = PlayerPrefs.GetFloat("Volume");
        AudioListener.volume = value;    
    }

    public void PlaySound (AudioClip clip)
    {
        _soundEffect.PlayOneShot(clip);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }
}
