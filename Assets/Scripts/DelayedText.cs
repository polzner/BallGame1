using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DelayedText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _originalText;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        StartCoroutine(StartType());
    }

    private IEnumerator StartType()
    {
        _text.text = "";            
        var delay = new WaitForSeconds(_delay);

        foreach (var symbol in _originalText)
        {
            _text.text += symbol;
            yield return delay;
        }
    }
}
