using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject LevelWinPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.Instance.SubscribeEvent(EventType.LoadLevel, () => ShowPanel(levelPanel));
        GameManager.Instance.SubscribeEvent(EventType.LevelWin, () => ShowPanel(LevelWinPanel));
        GameManager.Instance.SubscribeEvent(EventType.GameOver, () => ShowPanel(gameOverPanel));
        GameManager.Instance.SubscribeEvent(EventType.GoHome, () => ShowPanel(homePanel));
    }

    private void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }

    private void HideAllPanels()
    {
        homePanel.SetActive(false);
        levelPanel.SetActive(false);
        LevelWinPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
}
