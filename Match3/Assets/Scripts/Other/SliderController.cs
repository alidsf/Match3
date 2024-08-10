using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;

    private float _speed = 10;
    private int _currentValue = 0;

    private void Update()
    {
        slider.value = Mathf.MoveTowards(slider.value, _currentValue, _speed * Time.deltaTime);
        text.text = $"{(int)slider.value} / {slider.maxValue}";
    }

    public void Init(int maxValue) =>
        slider.maxValue = maxValue;

    public void SetValue(int value) =>
        _currentValue = value;

    public void Reset()
    {
        _currentValue = 0;
        slider.value = 0;
        text.text = "";
    }
}
