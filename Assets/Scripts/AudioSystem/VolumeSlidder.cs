using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidder : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            _slider.value = 0.5f;
            AudioManager.Instance.ChangeVolume(_slider.value);
            _slider.onValueChanged.AddListener(value => AudioManager.Instance.ChangeVolume(value));
            PlayerPrefs.SetFloat("Volume", _slider.value);
        }
        else
        {
            _slider.value = PlayerPrefs.GetFloat("Volume");
            AudioManager.Instance.ChangeVolume(_slider.value);
            _slider.onValueChanged.AddListener(value => AudioManager.Instance.ChangeVolume(value));
        }
    }
}
