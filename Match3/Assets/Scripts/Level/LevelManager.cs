using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private SliderController sliderController;
    [SerializeField] private Timer timer;

    LevelScriptableObject _currentLevel;
    private bool _isPlaying;

    private void Awake()
    {
        GameManager.Instance.SubscribeEvent(EventType.LoadLevel, LoadLevel);
        GameManager.Instance.SubscribeEvent(EventType.LevelWin, Reset);
        GameManager.Instance.SubscribeEvent(EventType.GameOver, Reset);
    }

    private void Update()
    {
        if (_isPlaying)
        {
            sliderController.SetValue(gridManager.numberOfMatches);

            if (gridManager.numberOfMatches >= _currentLevel.TargetScore)
                GameManager.Instance.LevelWin();
            else if (!timer.TimeCalculation())
                GameManager.Instance.GameOver();
        }
    }

    private void LoadLevel()
    {
        _currentLevel = GetLevel(1);

        gridManager.Init(_currentLevel.Column, _currentLevel.Row, _currentLevel.BoardSize, _currentLevel.StartPosition);

        sliderController.Init(_currentLevel.TargetScore);
        timer.Init(_currentLevel.Duration);

        _isPlaying = true;
    }

    private LevelScriptableObject GetLevel(int levelNumber)
    {
        return Resources.Load<LevelScriptableObject>("Levels/" + levelNumber);
    }

    private void Reset()
    {
        _isPlaying = false;
        gridManager.Reset();
        sliderController.Reset();
        timer.Reset();
    }
}
