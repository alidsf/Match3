using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelWinButton;
    [SerializeField] private Button gameOverButton;

    private EventManager _eventManager = new EventManager();

    private void Start()
    {
        playButton.onClick.AddListener(LoadLevel);
        levelWinButton.onClick.AddListener(GoHome);
        gameOverButton.onClick.AddListener(GoHome);

        GoHome();
    }

    public void SubscribeEvent(EventType eventType, UnityAction listener) =>
        _eventManager.SubscribeEvent(eventType, listener);

    private void LoadLevel()
    {
        _eventManager.Invoke(EventType.LoadLevel);
    }

    public void LevelWin()
    {
        _eventManager.Invoke(EventType.LevelWin);
    }

    public void GameOver()
    {
        _eventManager.Invoke(EventType.GameOver);
    }

    private void GoHome()
    {
        _eventManager.Invoke(EventType.GoHome);
    }

}
