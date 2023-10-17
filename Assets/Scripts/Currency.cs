using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private float currency;
    public event Action OnCurrencyChanged;
    
    public float GetCurrency()
    {
        return currency;
    }
    public void AddCurrency(float amount)
    {
        if(amount < 0)
            throw new Exception("Cannot add negative currency");
        currency += amount;  
        OnCurrencyChanged?.Invoke();
    }
    
    public void SpendCurrency(float amount)
    {
        if(amount < 0)
            throw new Exception("Cannot spend negative currency");
        
        if (currency >= amount)
        {
            currency -= amount;
            OnCurrencyChanged?.Invoke();
        }
    }
}
