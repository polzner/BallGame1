using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _healthText;

    private void OnEnable()
    {
        _player.HealthChanged += DrawHealth;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= DrawHealth;
    }

    private void DrawHealth(int health)
    {
        _healthText.text = health.ToString();
    }
}
