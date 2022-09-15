using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidder : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private void Start()
    {
        AudioManager.Instance.ChangeVolume(_slider.value);
        _slider.onValueChanged.AddListener(value => AudioManager.Instance.ChangeVolume(value));
    }
}
