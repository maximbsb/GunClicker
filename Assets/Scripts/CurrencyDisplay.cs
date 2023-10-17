using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private Currency currency;
    [SerializeField] private TMP_Text currencyText;

    private void Start()
    {
        currency.OnCurrencyChanged += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        currencyText.text = currency.GetCurrency().ToString("F0");
    }

    private void Reset()
    {
        currency = FindObjectOfType<Currency>();
        currencyText = GetComponent<TMP_Text>();
    }
}
