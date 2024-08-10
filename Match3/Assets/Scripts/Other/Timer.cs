using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private float _currentTime;

    public void Init(int time) =>
        _currentTime = time;

    public bool TimeCalculation()
    {
        _currentTime -= Time.deltaTime;
        text.text = ((int)_currentTime).ToString();

        return _currentTime > 0;
    }

    public void Reset()
    {
        _currentTime = 0;
        text.text = "";
    }
}
