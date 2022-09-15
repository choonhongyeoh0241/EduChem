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
    }

    public void PlaySound (AudioClip clip)
    {
        _soundEffect.PlayOneShot(clip);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.Save();
    }

    public void ToggleEffect()
    {
        _soundEffect.mute = !_soundEffect.mute;
        PlayerPrefs.Save();
    }

    public void ToggleMusic()
    {
        _music.mute = !_music.mute;
        PlayerPrefs.Save();
    }
}