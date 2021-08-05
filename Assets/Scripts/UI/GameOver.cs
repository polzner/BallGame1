using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _restart;

    private void OnEnable()
    {
        _player.Dead += ActivateGameOverPanel;
        _restart.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable()
    {
        _player.Dead -= ActivateGameOverPanel;
        _restart.onClick.RemoveListener(BackToMainMenu);
    }

    private void ActivateGameOverPanel()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    private void BackToMainMenu()
    {
        MainMenu.Load();
        Time.timeScale = 1;
    }
}
