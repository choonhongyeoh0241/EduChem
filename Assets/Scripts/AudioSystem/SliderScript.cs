using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    
    private void Start()
    {
        _slider.onValueChanged.AddListener((value) => {
            value *= 100;
            _sliderText.text = value.ToString("0");
        });
    }
}
