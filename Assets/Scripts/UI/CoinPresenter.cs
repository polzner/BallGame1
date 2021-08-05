using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPresenter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinsText;

    private void OnEnable()
    {
        _player.CoinQuantityChanged += DisplayCoins;
    }

    private void OnDisable()
    {
        _player.CoinQuantityChanged -= DisplayCoins;
    }

    private void DisplayCoins(int coinQuantity)
    {
        _coinsText.text = coinQuantity.ToString();
    }
}
